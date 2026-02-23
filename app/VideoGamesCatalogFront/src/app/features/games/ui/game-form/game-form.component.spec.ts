import { ComponentFixture, TestBed } from '@angular/core/testing';
import { GameFormComponent } from './game-form.component';
import { GameModel } from '../../data-access/game.model';
import { ComponentRef } from '@angular/core';

describe('GameFormComponent', () => {
  let component: GameFormComponent;
  let componentRef: ComponentRef<GameFormComponent>;
  let fixture: ComponentFixture<GameFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GameFormComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(GameFormComponent);
    component = fixture.componentInstance;
    componentRef = fixture.componentRef;
  });

  describe('Form Initialization', () => {
    it('should create with empty form when no initial data', () => {
      fixture.detectChanges();

      expect(component.form.get('title')?.value).toBe('');
      expect(component.form.get('description')?.value).toBe('');
    });

    it('should populate form with initial data', () => {
      const initialData: GameModel = {
        id: '123',
        title: 'Test Game',
        description: 'Test Description',
        rowVersion: 'abc',
      };
      componentRef.setInput('initialData', initialData);
      fixture.detectChanges();

      expect(component.form.get('title')?.value).toBe('Test Game');
      expect(component.form.get('description')?.value).toBe('Test Description');
    });
  });

  describe('Title Validation', () => {
    beforeEach(() => {
      fixture.detectChanges();
    });

    it('should require title', () => {
      const titleControl = component.form.get('title');
      titleControl?.setValue('');
      titleControl?.markAsTouched();

      expect(titleControl?.hasError('required')).toBeTrue();
      expect(component.form.valid).toBeFalse();
    });

    it('should accept valid title', () => {
      const titleControl = component.form.get('title');
      titleControl?.setValue('Valid Title');

      expect(titleControl?.hasError('required')).toBeFalse();
      expect(titleControl?.valid).toBeTrue();
    });

    it('should reject title exceeding 200 characters', () => {
      const titleControl = component.form.get('title');
      titleControl?.setValue('a'.repeat(201));

      expect(titleControl?.hasError('maxlength')).toBeTrue();
    });

    it('should accept title with exactly 200 characters', () => {
      const titleControl = component.form.get('title');
      titleControl?.setValue('a'.repeat(200));

      expect(titleControl?.hasError('maxlength')).toBeFalse();
    });
  });

  describe('Description Validation', () => {
    beforeEach(() => {
      fixture.detectChanges();
    });

    it('should allow empty description', () => {
      const descControl = component.form.get('description');
      descControl?.setValue('');

      expect(descControl?.valid).toBeTrue();
    });

    it('should reject description exceeding 2000 characters', () => {
      const descControl = component.form.get('description');
      descControl?.setValue('a'.repeat(2001));

      expect(descControl?.hasError('maxlength')).toBeTrue();
    });

    it('should accept description with exactly 2000 characters', () => {
      const descControl = component.form.get('description');
      descControl?.setValue('a'.repeat(2000));

      expect(descControl?.hasError('maxlength')).toBeFalse();
    });
  });

  describe('isFieldInvalid', () => {
    beforeEach(() => {
      fixture.detectChanges();
    });

    it('should return false for valid field', () => {
      component.form.get('title')?.setValue('Valid');
      component.form.get('title')?.markAsTouched();

      expect(component.isFieldInvalid('title')).toBeFalse();
    });

    it('should return true for invalid touched field', () => {
      component.form.get('title')?.setValue('');
      component.form.get('title')?.markAsTouched();

      expect(component.isFieldInvalid('title')).toBeTrue();
    });

    it('should return false for invalid untouched field', () => {
      component.form.get('title')?.setValue('');

      expect(component.isFieldInvalid('title')).toBeFalse();
    });

    it('should return true for invalid dirty field', () => {
      component.form.get('title')?.setValue('');
      component.form.get('title')?.markAsDirty();

      expect(component.isFieldInvalid('title')).toBeTrue();
    });
  });

  describe('Form Submission', () => {
    beforeEach(() => {
      fixture.detectChanges();
    });

    it('should emit save event with form value when valid', () => {
      const saveSpy = spyOn(component.save, 'emit');

      component.form.get('title')?.setValue('Test Game');
      component.form.get('description')?.setValue('Test Description');
      component.onSubmit();

      expect(saveSpy).toHaveBeenCalledWith({
        title: 'Test Game',
        description: 'Test Description',
      });
    });

    it('should not emit save event when form is invalid', () => {
      const saveSpy = spyOn(component.save, 'emit');

      component.form.get('title')?.setValue('');
      component.onSubmit();

      expect(saveSpy).not.toHaveBeenCalled();
    });
  });

  describe('Cancel', () => {
    beforeEach(() => {
      fixture.detectChanges();
    });

    it('should emit cancel event when cancel button clicked', () => {
      const cancelSpy = spyOn(component.cancel, 'emit');

      component.cancel.emit();

      expect(cancelSpy).toHaveBeenCalled();
    });
  });
});
