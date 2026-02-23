import { TestBed } from '@angular/core/testing';
import { firstValueFrom } from 'rxjs';
import { GamesService } from './games.service';
import { Api } from '../../../core/api/generated';
import { GameFormValue } from './game.model';
import { HttpResponse } from '../../../core/api/generated/http-client';
import { VideoGameResponse } from '../../../core/api/generated';

describe('GamesService', () => {
  let service: GamesService;
  let apiSpy: jasmine.SpyObj<Api>;

  const mockGameResponse: VideoGameResponse = {
    id: '123',
    title: 'Test Game',
    description: 'Test Description',
    rowVersion: 'abc123',
  };

  const mockHttpResponse = async <T>(
    data: T,
  ): Promise<HttpResponse<T, unknown>> =>
    ({
      data,
    }) as HttpResponse<T, unknown>;

  beforeEach(() => {
    apiSpy = jasmine.createSpyObj('Api', [
      'videoGameList',
      'videoGameDetail',
      'videoGameCreate',
      'videoGameUpdate',
      'videoGameDelete',
    ]);

    TestBed.configureTestingModule({
      providers: [GamesService, { provide: Api, useValue: apiSpy }],
    });

    service = TestBed.inject(GamesService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  describe('getAll', () => {
    it('should return mapped games', async () => {
      apiSpy.videoGameList.and.returnValue(
        mockHttpResponse([mockGameResponse]),
      );

      const games = await firstValueFrom(service.getAll());

      expect(games).toEqual([
        {
          id: '123',
          title: 'Test Game',
          description: 'Test Description',
          rowVersion: 'abc123',
        },
      ]);
    });

    it('should handle empty response', async () => {
      apiSpy.videoGameList.and.returnValue(mockHttpResponse([]));

      const games = await firstValueFrom(service.getAll());

      expect(games).toEqual([]);
    });
  });

  describe('getById', () => {
    it('should return mapped game', async () => {
      apiSpy.videoGameDetail.and.returnValue(
        mockHttpResponse(mockGameResponse),
      );

      const game = await firstValueFrom(service.getById('123'));

      expect(apiSpy.videoGameDetail).toHaveBeenCalledWith('123');
      expect(game).toEqual({
        id: '123',
        title: 'Test Game',
        description: 'Test Description',
        rowVersion: 'abc123',
      });
    });
  });

  describe('add', () => {
    it('should call API with mapped request', async () => {
      const formValue: GameFormValue = {
        title: 'New Game',
        description: 'New Description',
      };
      apiSpy.videoGameCreate.and.returnValue(mockHttpResponse('new-id'));

      const id = await firstValueFrom(service.add(formValue));

      expect(apiSpy.videoGameCreate).toHaveBeenCalledWith({
        title: 'New Game',
        description: 'New Description',
      });
      expect(id).toBe('new-id');
    });
  });

  describe('update', () => {
    it('should call API with mapped request including rowVersion', async () => {
      const formValue: GameFormValue = {
        title: 'Updated Game',
        description: 'Updated Description',
      };
      apiSpy.videoGameUpdate.and.returnValue(
        mockHttpResponse(undefined as void),
      );

      await firstValueFrom(service.update('123', formValue, 'version-1'));

      expect(apiSpy.videoGameUpdate).toHaveBeenCalledWith('123', {
        title: 'Updated Game',
        description: 'Updated Description',
        rowVersion: 'version-1',
      });
    });

    it('should handle null rowVersion', async () => {
      const formValue: GameFormValue = {
        title: 'Updated Game',
        description: 'Updated Description',
      };
      apiSpy.videoGameUpdate.and.returnValue(
        mockHttpResponse(undefined as void),
      );

      await firstValueFrom(service.update('123', formValue, null));

      expect(apiSpy.videoGameUpdate).toHaveBeenCalledWith('123', {
        title: 'Updated Game',
        description: 'Updated Description',
        rowVersion: null,
      });
    });
  });

  describe('delete', () => {
    it('should call API delete', async () => {
      apiSpy.videoGameDelete.and.returnValue(
        mockHttpResponse(undefined as void),
      );

      await firstValueFrom(service.delete('123'));

      expect(apiSpy.videoGameDelete).toHaveBeenCalledWith('123');
    });
  });
});
