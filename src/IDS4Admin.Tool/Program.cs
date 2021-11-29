// See https://aka.ms/new-console-template for more information

using IDS4Admin.Tool;

var command = "";
var config = IDS4Config.Load("");
do
{
    DisplayCommand();
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.Write(">");
    command = Console.ReadLine();
    TreateCommand(command);
    Console.ResetColor();
} while (command != "exit");



string DisplayCommand()
{
    Console.WriteLine("请输入命令1-5，输入exit退出");
    Console.WriteLine("1:确定STS（认证服务STS）运行的Uri:" + config.STSUri);
    Console.WriteLine("2:确定管理App(AdminApp)运行的Uri:" + config.AdminUri);
    Console.WriteLine("3:数据库类型:" + config.DbType);
    Console.WriteLine("4:数据库连接字符串:" + config.DbConnString);
    Console.WriteLine("5:生成配置文件...");
    Console.WriteLine("exit:退出");
        return "";
}

void TreateCommand(string? command)
{
    switch (command)
    {
        case "1":
            config.STSUri = GetNewValue("STS Uri");
            config.SaveToFile("");
            break;
        case "2":
            config.AdminUri = GetNewValue("Admin Uri");
            config.SaveToFile("");
            break;
        case "3":
            config.DbType = GetNewValue("DbType");
            config.SaveToFile("");

            break;
        case "4":
            config.DbConnString = GetNewValue("DbConnString");
            config.SaveToFile("");
            break;
        case "5":
            Console.WriteLine(Utility.CreateConfigureFiles(config));
            break;
        case "exit":
            break;
        default:
            Console.WriteLine("请输入1-6或者exit");
            break;
    }

    string GetNewValue(string valuename)
    {
        Console.WriteLine("请输入变量值 " + valuename);
        var newval = Console.ReadLine();
        while (string.IsNullOrEmpty(newval))
        {
            Console.WriteLine("值不能为空");
            newval = Console.ReadLine();
        }
        return newval;
    }
}