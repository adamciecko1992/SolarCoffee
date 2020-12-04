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
      trimmedString.length > 3;
    const message = "Can contain only letters and spaces(min 4, max 20)";
    return [valid, message];
  }
  static onlyLettersNoSpaces(str: string): boolean {
    //Firstname,Lastname
    const lowerCased = str.toLowerCase();
    return (
      /^[A-Za-z]+$/.test(lowerCased) &&
      lowerCased.length < 20 &&
      lowerCased.length > 3 &&
      !lowerCased.includes(" ")
    );
  }
  static onlyNumbers(input: string | number): boolean {
    //postal-code
    const str = input.toString().replaceAll("-", "");
    return /^[0-9]+$/.test(str) && str.length <= 10 && str.length >= 4;
  }
  static onlyLettersAndNumbers(str: string): boolean {
    const lowerCased = str.toLowerCase();
    return (
      /^[A-Za-z0-9]+$/.test(lowerCased) &&
      lowerCased.length < 20 &&
      lowerCased.length > 3 &&
      !lowerCased.includes(" ")
    );
  }
  static onlyLettersAndNumbersWithSpaces(str: string): boolean {
    const lowerCased = str.toLowerCase().replaceAll(" ", "");
    return (
      /^[A-Za-z0-9]+$/.test(lowerCased) &&
      lowerCased.length < 20 &&
      lowerCased.length > 3 &&
      !lowerCased.includes(" ")
    );
  }
}

export default Validators;
