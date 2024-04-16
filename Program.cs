using System;

namespace BreweryAutomation
{
   public class BeerEncapsulator
   {
      private decimal _availableBeerVolume; // Volume de bière disponible en centilitres
      private int _availableBottles;         // Nombre de bouteilles disponibles
      private int _availableCapsules;        // Nombre de capsules disponibles

      public BeerEncapsulator(decimal beerVolume, int bottles, int capsules)
      {
         _availableBeerVolume = beerVolume;
         _availableBottles = bottles;
         _availableCapsules = capsules;
      }

      // Accesseur pour obtenir le volume de bière disponible
      public decimal AvailableBeerVolume
      {
         get => _availableBeerVolume;
      }

      // Accesseur pour le nombre de bouteilles
      public int AvailableBottles
      {
         get => _availableBottles;
         set => _availableBottles = Math.Max(0, value); // Empêche les valeurs négatives
      }

      // Accesseur pour le nombre de capsules
      public int AvailableCapsules
      {
         get => _availableCapsules;
         set => _availableCapsules = Math.Max(0, value); // Empêche les valeurs négatives
      }

      // Méthode pour ajouter de la bière
      public void AddBeer(decimal beerVolume)
      {
         _availableBeerVolume += beerVolume;
      }

      // Méthode pour produire des bouteilles de bière encapsulées
      public int ProduceEncapsulatedBeerBottles(int numberOfBottles)
      {
         int possibleBottles = Math.Min(Math.Min(_availableBottles, _availableCapsules),
                                        (int)(_availableBeerVolume / 33));

         if (possibleBottles < numberOfBottles)
         {
            Console.WriteLine($"Insufficient resources to produce {numberOfBottles} bottles.");
            if (_availableBottles < numberOfBottles)
               Console.WriteLine($"Add more bottles. Required: {numberOfBottles - _availableBottles}");
            if (_availableCapsules < numberOfBottles)
               Console.WriteLine($"Add more capsules. Required: {numberOfBottles - _availableCapsules}");
            if (_availableBeerVolume < numberOfBottles * 33)
               Console.WriteLine($"Add more beer. Required: {numberOfBottles * 33 - _availableBeerVolume} cl");

            return 0;
         }

         _availableBottles -= numberOfBottles;
         _availableCapsules -= numberOfBottles;
         _availableBeerVolume -= numberOfBottles * 33;

         return numberOfBottles;
      }
   }

   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine("Enter the initial amount of beer in liters:");
         decimal initialBeer = decimal.Parse(Console.ReadLine()) * 100; // Convert liters to centiliters

         Console.WriteLine("Enter the number of empty bottles available:");
         int initialBottles = int.Parse(Console.ReadLine());

         Console.WriteLine("Enter the number of capsules available:");
         int initialCapsules = int.Parse(Console.ReadLine());

         BeerEncapsulator encapsulator = new BeerEncapsulator(initialBeer, initialBottles, initialCapsules);

         Console.WriteLine("How many bottles would you like to produce?");
         int targetBottles = int.Parse(Console.ReadLine());

         int produced = encapsulator.ProduceEncapsulatedBeerBottles(targetBottles);
         Console.WriteLine($"Produced {produced} bottles.");

         Console.WriteLine($"Remaining beer: {encapsulator.AvailableBeerVolume / 100} liters");
         Console.WriteLine($"Remaining bottles: {encapsulator.AvailableBottles}");
         Console.WriteLine($"Remaining capsules: {encapsulator.AvailableCapsules}");
      }
   }
}

