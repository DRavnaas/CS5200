package testAzureSqlApp;

public class TestMain {

	public static void main(String[] args) 
	{
		String message = AzureSqlBase.GetMessageFromDb();
		
		System.out.println(message);
	}
}
