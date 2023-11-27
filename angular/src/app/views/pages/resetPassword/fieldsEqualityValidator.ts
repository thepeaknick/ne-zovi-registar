import { AbstractControl, ValidationErrors, ValidatorFn } from '@angular/forms';

export function validateFieldsEquality(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        const rootControl = control.root;
        if (!rootControl || !(rootControl instanceof AbstractControl)) {
            return null; // Return null if root control is not found or not an instance of AbstractControl
        }

        // Access the form fields' values using rootControl.get(<fieldName>)
        const password1Value = rootControl.get("newPassword1")?.value;
        const password2Value = rootControl.get("newPassword2")?.value;

        console.log("Validator za jednakost polja 1", password1Value)
        console.log("Validator za jednakost polja 2", password2Value)

        // Implement your validation logic here
        if (!(password1Value && password2Value && password1Value === password2Value)) {
            console.log('Field values not match.' )
            return { fieldEquality: { message: 'Field values should match.' } };
        }

        // If validation passes, return null
        return null;
    }
};