namespace NationalEducation
{
    internal static class ConstantValue
    {
        public const string INDENT_VALUE = "{0, -19} : {1}";
        public const string TABULATION = "    ";
        public const string SEPARATION = "----------------------------------------------------------------------";

        public const string NAME_PATTERN = "^([A-Z]|[a-z])[a-z]{2,}(-([A-Z]|[a-z])[a-z]{2,})?$";
        public const string NAME_ERROR_MESSAGE = "Vous êtes limité à l'alphabet et au caractère spécial « - ».\n";

        public const string OBSERVATION_PATTERN = "^([A-Z]|[a-z])[a-z ]{2,}$";
        public const string OBSERVATION_ERROR_MESSAGE = "Vous êtes limité à l'alphabet et à l'espace.\n";

        public const string DATE_FORMAT = "dd/MM/yyyy";
        public const string DATE_ERROR_MESSAGE = "Vous devez respecter le format jj/mm/aaaa.\n";

        public const float MAX_GRADE = 20.0f;

        public const string NO_STUDENTS_ERROR_MESSAGE = "Il n'y a pas d'étudiant pour le moment.\n";
        public const string NO_COURSES_ERROR_MESSAGE = "Il n'y a pas de cours pour le moment.\n";

        // public const string PROJECT_PATH = "C:\\Users\\antho\\source\\repos\\NationalEducation\\NationalEducation";
        public const string PROJECT_PATH = "C:\\Users\\antho\\Source\\Repos\\abt-111\\NationalEducation\\NationalEducation";
        public const string JSON_FILE_NAME = "save.JSON";
        public const string LOG_FILE_NAME = "log.txt";
        public const string JSON_FILE_PATH = $"{PROJECT_PATH}\\{JSON_FILE_NAME}";
        public const string LOG_FILE_PATH = $"{PROJECT_PATH}\\{LOG_FILE_NAME}";
    }
}
