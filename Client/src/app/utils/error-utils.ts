export class ErrorUtils {
  static parseError(error: any): string {
    try {
      if (error?.error) {
        const parsedError =
          typeof error.error === 'string'
            ? JSON.parse(error.error)
            : error.error;

        if (parsedError.message) return parsedError.message;

        if (parsedError.errors) {
          return Object.entries(parsedError.errors)
            .map(
              ([field, messages]) =>
                `${field}: ${(messages as string[]).join(', ')}`
            )
            .join('\n');
        }
      }
    } catch (err) {
      console.error('Error parsing API error:', err);
      return 'Error processing server response';
    }
    return 'Invalid request';
  }
}
