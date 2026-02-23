import { ComponentFixture, TestBed } from '@angular/core/testing';
import { GamesTableComponent } from './games-table.component';
import { GameModel } from '../../data-access/game.model';

describe('GamesTableComponent', () => {
  let fixture: ComponentFixture<GamesTableComponent>;
  let component: GamesTableComponent;

  const mockGames: GameModel[] = [
    { id: '1', title: 'Zelda', description: 'Adventure', rowVersion: 'a' },
    { id: '2', title: 'Mario', description: 'Platformer', rowVersion: 'b' },
  ];

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GamesTableComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(GamesTableComponent);
    component = fixture.componentInstance;
  });

  it('should render spinner when loading', () => {
    fixture.componentRef.setInput('loading', true);
    fixture.detectChanges();

    expect(
      fixture.nativeElement.querySelector('app-loading-spinner'),
    ).toBeTruthy();
  });

  it('should render empty state when no games', () => {
    fixture.componentRef.setInput('games', []);
    fixture.detectChanges();

    const alert: HTMLElement | null =
      fixture.nativeElement.querySelector('.alert-info');
    expect(alert?.textContent).toContain('No games found');
  });

  it('should render rows and emit update/remove events', () => {
    const updateSpy = jasmine.createSpy('update');
    const removeSpy = jasmine.createSpy('remove');
    component.update.subscribe(updateSpy);
    component.remove.subscribe(removeSpy);

    fixture.componentRef.setInput('games', mockGames);
    fixture.detectChanges();

    const rows = fixture.nativeElement.querySelectorAll('tbody tr');
    expect(rows.length).toBe(mockGames.length);

    const firstRowButtons = rows[0].querySelectorAll('button');
    firstRowButtons[0].dispatchEvent(new Event('click'));
    firstRowButtons[1].dispatchEvent(new Event('click'));

    expect(updateSpy).toHaveBeenCalledWith(mockGames[0]);
    expect(removeSpy).toHaveBeenCalledWith(mockGames[0]);
  });
});
