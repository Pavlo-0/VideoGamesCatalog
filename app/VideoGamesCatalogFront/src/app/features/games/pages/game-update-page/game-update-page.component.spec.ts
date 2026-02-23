import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router, ActivatedRoute, convertToParamMap } from '@angular/router';
import { of } from 'rxjs';
import { GameUpdatePageComponent } from './game-update-page.component';
import { GamesService } from '../../data-access/games.service';
import { GameModel, GameFormValue } from '../../data-access/game.model';

describe('GameUpdatePageComponent', () => {
  let gamesServiceSpy: jasmine.SpyObj<GamesService>;
  let routerSpy: jasmine.SpyObj<Router>;

  const mockGame: GameModel = {
    id: '1',
    title: 'Halo',
    description: 'Shooter',
    rowVersion: 'v1',
  };

  async function setup(routeId?: string) {
    TestBed.resetTestingModule();

    gamesServiceSpy = jasmine.createSpyObj('GamesService', [
      'getById',
      'add',
      'update',
    ]);
    routerSpy = jasmine.createSpyObj('Router', ['navigate']);

    gamesServiceSpy.getById.and.returnValue(of(mockGame));
    gamesServiceSpy.add.and.returnValue(of('new-id'));
    gamesServiceSpy.update.and.returnValue(of(void 0));

    await TestBed.configureTestingModule({
      imports: [GameUpdatePageComponent],
      providers: [
        { provide: GamesService, useValue: gamesServiceSpy },
        { provide: Router, useValue: routerSpy },
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: {
              paramMap: convertToParamMap(routeId ? { id: routeId } : {}),
            },
          },
        },
      ],
    }).compileComponents();

    const fixture: ComponentFixture<GameUpdatePageComponent> =
      TestBed.createComponent(GameUpdatePageComponent);
    const component = fixture.componentInstance;
    fixture.detectChanges();
    await fixture.whenStable();
    return { fixture, component };
  }

  it('should initialize in add mode when no id', async () => {
    const { component } = await setup();
    expect(component.isAdd).toBeTrue();
    expect(gamesServiceSpy.getById).not.toHaveBeenCalled();
  });

  it('should load existing game when id provided', async () => {
    const { component } = await setup('1');
    expect(gamesServiceSpy.getById).toHaveBeenCalledWith('1');
    expect(component.game()).toEqual(mockGame);
  });

  it('should add new game and navigate away', async () => {
    const { component } = await setup();
    const formValue: GameFormValue = {
      title: 'New Game',
      description: 'Desc',
    };

    component.onSave(formValue);

    expect(gamesServiceSpy.add).toHaveBeenCalledWith(formValue);
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/games']);
  });

  it('should update game and navigate away', async () => {
    const { component } = await setup('1');
    const formValue: GameFormValue = {
      title: 'Updated',
      description: 'Desc',
    };

    component.onSave(formValue);

    expect(gamesServiceSpy.update).toHaveBeenCalledWith('1', formValue, 'v1');
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/games']);
  });

  it('should navigate back on cancel', async () => {
    const { component } = await setup();
    component.onCancel();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/games']);
  });
});
