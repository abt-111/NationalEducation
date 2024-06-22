namespace NationalEducation
{
    internal static class ConstantValue
    {
        public const string NAME_PATTERN = "^([A-Z]|[a-z])[a-z]{2,}(-([A-Z]|[a-z])[a-z]{2,})?$";
        public const string NAME_ERROR_MESSAGE = "Vous êtes limité à l'alphabet et au caractère spécial « - ».\n";

        public const string DATE_FORMAT = "dd/MM/yyyy";
        public const string DATE_ERROR_MESSAGE = "Vous devez respecter le format jj/mm/aaaa.\n";

        public const float MAX_GRADE = 20.0f;
    }
}
