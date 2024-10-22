namespace DocOps.Results
{
    public class CommandResult
    {

        public bool Success { get; set; }

        public required string Message { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        
        public static CommandResult SuccessResult(string message)
        {
            return new CommandResult
            {
                Success = true,
                Message = message
            };
        }

        public static CommandResult ErrorResult(string message, string error)
        {
            return new CommandResult
            {
                Success = false,
                Message = message,
                Errors = new List<string> { error } 
            };
        }

        public static CommandResult ErrorResult(string message, List<string> errors)
        {
            return new CommandResult
            {
                Success = false,
                Message = message,
                Errors = errors
            };
        }
    }
}