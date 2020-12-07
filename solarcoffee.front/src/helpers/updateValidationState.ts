export default function updateValid<T>(
    value: boolean,
    property: keyof T,
    objectReference: T
) {
    (objectReference as any)[property] = value;
}


