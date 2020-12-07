export default function updateValid<T>(
    value: boolean,
    property: keyof T,
    objectReference: T
) {
    (objectReference as any)[property] = value;
    const validationStateValues = Object.values(objectReference);
    const formValid = validationStateValues.every((val) => val);
    // if (formValid) {
    //   valid.value = true;
    // }
}


