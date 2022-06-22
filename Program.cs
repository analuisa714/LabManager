using Microsoft.Data.Sqlite;
using LabManager.DataBase;
using LabManager.Repositories; //importar a classe 
using LabManager.Models;

var databaseConfig = new DatabaseConfig(); //objeto. não precisa ficar criando nas classes

var databaseSetup = new DatabaseSetup(databaseConfig);

var computerRepository = new ComputerRepository(databaseConfig); //criar a váriavel 
var labRepository = new LabRepository(databaseConfig);

//Routing
var modelName = args[0];
var modelAction = args[1];

if (modelName == "Computer")
{
        if (modelAction == "List")
        {
                Console.WriteLine("Computer List");
                foreach (var computer in computerRepository.GetAll())
                {
                        Console.WriteLine(
                                "{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor
                                );
                }
        }

        if (modelAction == "New") 
        {
                var id = Convert.ToInt32(args [2]);
                var ram = args [3];
                var processor = args [4];

                var computer = new Computer(id, ram, processor);
                computerRepository.Save(computer);
        }
        
        if (modelAction == "Show") 
        {
                var id = Convert.ToInt32(args [2]);
                var computer = computerRepository.GetById(id);
                Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
        }

        if (modelAction == "Delete") 
        {
                var id = Convert.ToInt32(args [2]);
                computerRepository.Delete(id);
        }

        if (modelAction == "Update") 
        {
                var id = Convert.ToInt32(args [2]);
                var ram = args [3];
                var processor = args [4];

                var computer = new Computer(id, ram, processor);
                computerRepository.Update(computer);
                Console.WriteLine("{0}, {1}, {2}", computer.Id, computer.Ram, computer.Processor);
        }
}

if(modelName == "Lab")
{
    if(modelAction == "List")
    {
        foreach (var lab in labRepository.GetAll())
        {
            Console.WriteLine("{0}, {1}, {2}, {3}", lab.Id, lab.Number, lab.Name, lab.Block);
        }
    }

    if(modelAction == "New")
    {
        var id = Convert.ToInt32(args[2]);
        var number = Convert.ToInt32(args[3]);
        var name = args[4];
        var block = args[5];
        Console.WriteLine("New Lab");
        Console.WriteLine("{0}, {1}, {2}, {3}", id, number, name, block);

        var lab = new Lab(id, number, name, block);
        labRepository.Save(lab);
    }

    if (modelAction == "Show")
    {
        var id = Convert.ToInt32(args[2]);
        var lab = labRepository.GetById(id);
        Console.WriteLine("{0}, {1}, {2}, {3}", lab.Id, lab.Number, lab.Name, lab.Block);
    }

    if (modelAction == "Update")
    {
        var id = Convert.ToInt32(args[2]);
        var number = Convert.ToInt32(args[3]);
        var name = args[4];
        var block = args[5];
        var lab = new Lab(id, number, name, block);

        labRepository.Update(lab);
    }

    if (modelAction == "Delete")
    {
        var id = Convert.ToInt32(args[2]);
        labRepository.Delete(id);
    }
}