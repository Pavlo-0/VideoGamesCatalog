import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ConfirmModalComponent } from './confirm-modal.component';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

describe('ConfirmModalComponent', () => {
  let fixture: ComponentFixture<ConfirmModalComponent>;
  let component: ConfirmModalComponent;
  let modalSpy: jasmine.SpyObj<NgbActiveModal>;

  beforeEach(async () => {
    modalSpy = jasmine.createSpyObj('NgbActiveModal', ['close', 'dismiss']);

    await TestBed.configureTestingModule({
      imports: [ConfirmModalComponent],
      providers: [{ provide: NgbActiveModal, useValue: modalSpy }],
    }).compileComponents();

    fixture = TestBed.createComponent(ConfirmModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should render default text', () => {
    const title = fixture.nativeElement.querySelector('.modal-title');
    const message = fixture.nativeElement.querySelector('.modal-body p');
    expect(title.textContent).toContain('Confirm');
    expect(message.textContent).toContain('Are you sure');
  });

  it('should dismiss when cancel clicked', () => {
    const cancelBtn = fixture.nativeElement.querySelector('.btn-secondary');
    cancelBtn.click();

    expect(modalSpy.dismiss).toHaveBeenCalled();
  });

  it('should close when confirm clicked', () => {
    const confirmBtn = fixture.nativeElement.querySelector('.btn-danger');
    confirmBtn.click();

    expect(modalSpy.close).toHaveBeenCalledWith(true);
  });
});
