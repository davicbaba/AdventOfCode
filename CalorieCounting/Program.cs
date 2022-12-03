
using CalorieCounting;

CalorieCounter calorieCounting = new CalorieCounter();

Console.WriteLine("Elfo que consumio mas calorias");

Console.WriteLine(calorieCounting.GetTheMostCalories());

Console.WriteLine("Suma del top tres de elfos que comieron mas calorias");

Console.WriteLine(calorieCounting.GetTopThreeCalories().Sum());
