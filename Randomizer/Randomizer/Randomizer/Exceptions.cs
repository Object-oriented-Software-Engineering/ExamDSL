namespace Randomizer;

public class RandomException : Exception { }

public class RandomNoValuesException : RandomException { }

public class PickerStrategyException : Exception { }

public class PickerUnableToPickException : PickerStrategyException { }

public class ValueException : Exception { }

public class ValueUnableToCompleteGetException : ValueException { }