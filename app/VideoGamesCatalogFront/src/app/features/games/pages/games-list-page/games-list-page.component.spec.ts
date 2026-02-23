import {
  ComponentFixture,
  TestBed,
  fakeAsync,
  tick,
} from '@angular/core/testing';
import { Router } from '@angular/router';
import { NgbModal, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { of } from 'rxjs';
import { GamesListPageComponent } from './games-list-page.component';
import { GamesService } from '../../data-access/games.service';
import { GameModel } from '../../data-access/game.model';

describe('GamesListPageComponent', () => {
  let fixture: ComponentFixture<GamesListPageComponent>;
  let component: GamesListPageComponent;
  let gamesServiceSpy: jasmine.SpyObj<GamesService>;
  let routerSpy: jasmine.SpyObj<Router>;
  let modalSpy: jasmine.SpyObj<NgbModal>;

  const mockGames: GameModel[] = [
    { id: '1', title: 'Halo', description: 'Shooter', rowVersion: 'a' },
  ];

  const createModalRef = (result: Promise<boolean>): NgbModalRef =>
    ({
      componentInstance: { title: '', message: '', confirmText: '' },
      result,
    }) as NgbModalRef;

  beforeEach(async () => {
    gamesServiceSpy = jasmine.createSpyObj('GamesService', [
      'getAll',
      'delete',
    ]);
    routerSpy = jasmine.createSpyObj('Router', ['navigate']);
    modalSpy = jasmine.createSpyObj('NgbModal', ['open']);

    gamesServiceSpy.getAll.and.returnValue(of(mockGames));
    gamesServiceSpy.delete.and.returnValue(of(void 0));

    await TestBed.configureTestingModule({
      imports: [GamesListPageComponent],
      providers: [
        { provide: GamesService, useValue: gamesServiceSpy },
        { provide: Router, useValue: routerSpy },
        { provide: NgbModal, useValue: modalSpy },
      ],
    }).compileComponents();

    fixture = TestBed.createComponent(GamesListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should load games on init', () => {
    expect(component.games()).toEqual(mockGames);
  });

  it('should navigate to add page', () => {
    component.onAdd();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/games', 'add']);
  });

  it('should navigate to update page', () => {
    component.onUpdate(mockGames[0]);
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/games', '1', 'update']);
  });

  it('should delete game when modal confirmed', fakeAsync(() => {
    modalSpy.open.and.returnValue(createModalRef(Promise.resolve(true)));

    component.onDelete(mockGames[0]);
    tick();

    expect(gamesServiceSpy.delete).toHaveBeenCalledWith('1');
  }));

  it('should not delete game when modal dismissed', fakeAsync(() => {
    modalSpy.open.and.returnValue(createModalRef(Promise.resolve(false)));

    component.onDelete(mockGames[0]);
    tick();

    expect(gamesServiceSpy.delete).not.toHaveBeenCalled();
  }));
});
