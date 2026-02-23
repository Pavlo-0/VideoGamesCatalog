import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NotFoundComponent } from './not-found.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('NotFoundComponent', () => {
  let fixture: ComponentFixture<NotFoundComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [RouterTestingModule, NotFoundComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(NotFoundComponent);
    fixture.detectChanges();
  });

  it('should render heading text', () => {
    const heading = fixture.nativeElement.querySelector('h2');
    expect(heading.textContent).toContain('Page Not Found');
  });

  it('should link back to games', () => {
    const link = fixture.nativeElement.querySelector('a[routerLink="/games"]');
    expect(link?.textContent).toContain('Go to Games');
  });
});
