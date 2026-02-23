import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LoadingSpinnerComponent } from './loading-spinner.component';

describe('LoadingSpinnerComponent', () => {
  let fixture: ComponentFixture<LoadingSpinnerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoadingSpinnerComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(LoadingSpinnerComponent);
    fixture.detectChanges();
  });

  it('should render spinner markup', () => {
    const spinner = fixture.nativeElement.querySelector('.spinner-border');
    expect(spinner).toBeTruthy();
    expect(spinner.getAttribute('role')).toBe('status');
  });

  it('should include accessible text', () => {
    const text = fixture.nativeElement.querySelector('.visually-hidden');
    expect(text?.textContent?.trim()).toBe('Loading...');
  });
});
