import { Directive } from '@angular/core';
import { NG_VALIDATORS, Validator, AbstractControl, ValidationErrors } from '@angular/forms';
import { validateFieldsEquality } from './fieldsEqualityValidator';

@Directive({
  selector: '[fieldsEquality]',
  providers: [
    {
      provide: NG_VALIDATORS,
      useExisting: FieldsEqualityValidatorDirective,
      multi: true,
    },
  ],
}) 

export class FieldsEqualityValidatorDirective implements Validator {
  validate(control: AbstractControl): ValidationErrors | null {
      console.log('bsldbalsdbalsbdas');
      return validateFieldsEquality()(control);
  }
}