package hellosqlazure;

public final class HelloSQLAzure 
{
	public static void main(String[] args) 
	{
		String message = SqlAzure.GetMessageFromDb();
		
		System.out.println(message);
	}
}
