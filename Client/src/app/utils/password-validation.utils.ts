export class PasswordValidationUtils {
  static hasUppercaseError(form: any): boolean {
    const control = form.get('password');
    return control?.hasError('pattern') && !/[A-Z]/.test(control.value);
  }

  static hasDigitError(form: any): boolean {
    const control = form.get('password');
    return control?.hasError('pattern') && !/\d/.test(control.value);
  }

  static hasSpecialCharError(form: any): boolean {
    const control = form.get('password');
    return control?.hasError('pattern') && !/[\W_]/.test(control.value);
  }
}
