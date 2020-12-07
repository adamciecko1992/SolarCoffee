class Validators {
  static onlyLettersWithSpaces(str: string): [boolean, string] {
    //City Country State
    const trimmedString = str
      .trim()
      .toLowerCase()
      .replaceAll(" ", "");

    const valid =
      /^[A-Za-z]+$/.test(trimmedString) &&
      trimmedString.length <= 20 &&
      trimmedString.length >= 2;
    const message = "Can contain only english letters and spaces(min 4, max 20)";
    return [valid, message];
  }
  static onlyLettersNoSpaces(str: string): [boolean, string] {
    //Firstname,Lastname
    const lowerCased = str.toLowerCase();
    const valid = (
      /^[A-Za-z]+$/.test(lowerCased) &&
      lowerCased.length < 20 &&
      lowerCased.length >= 2 &&
      !lowerCased.includes(" ")
    );
    const errorMessage = "Can contain only english letters with no spaces (min 2, max 20)";
    return [valid, errorMessage];

  }
  static onlyNumbersAndDashes(input: string | number): [boolean, string] {
    //postal-code
    const str = input.toString().replaceAll("-", "");
    const valid = /^[0-9]+$/.test(str) && str.length <= 10 && str.length >= 4;
    const errorMessage = "Can contain only numbers and dashes (min 4, max 10)";
    return [valid, errorMessage];
  }
  static onlyLettersAndNumbers(str: string): [boolean, string] {
    const lowerCased = str.toLowerCase();

    const valid = (
      /^[A-Za-z0-9]+$/.test(lowerCased) &&
      lowerCased.length <= 20 &&
      lowerCased.length >= 2 &&
      !lowerCased.includes(" ")
    );
    const errorMessage = "Can contain only english letters and numbers with no spaces (min 2, max 20)"
    return [valid, errorMessage]
  }
  static onlyLettersAndNumbersWithSpaces(str: string): [boolean, string] {
    const lowerCased = str.toLowerCase().replaceAll(" ", "").replaceAll("/", "");
    const valid = (
      /^[A-Za-z0-9]+$/.test(lowerCased) &&
      lowerCased.length <= 20 &&
      lowerCased.length >= 2 &&
      !lowerCased.includes(" ")
    );
    const errorMessage = "Can contain only numbers, / , english letters and spaces (min 2, max 20)"
    return [valid, errorMessage]
  }
  static onlyNumbers(input: string | number): [boolean, string] {
    const str = input.toString();
    const valid = /^[0-9]+$/.test(str) && str.length <= 10 && str.length >= 1;
    const errorMessage = "Can contain only numbers (min 1, max 10)";
    return [valid, errorMessage];
  }
}

export default Validators;
