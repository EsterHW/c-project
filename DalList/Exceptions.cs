namespace Dal;


public class DalException : Exception
{
    public DalException(string message) : base(message) { }
}


public class DalDoesNotExistException : DalException
{
    public DalDoesNotExistException(string message) : base(message) { }
}


public class DalAlreadyExistsException : DalException
{
    public DalAlreadyExistsException(string message) : base(message) { }
}

public class DalDeletionImpossibleException : DalException
{
    public DalDeletionImpossibleException(string message) : base(message) { }
}
