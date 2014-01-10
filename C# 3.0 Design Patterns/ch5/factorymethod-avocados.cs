  using System;
  using System.Collections;

  class FactoryPattern {
  
  // Factory Method Pattern       Judith Bishop 2006
  //  Example of exporting from different suppliers
    
  interface IProduct {
    string ShipFrom();
  }

  class ProductA : IProduct {
    public String ShipFrom () {
      return " from South Africa";
    }
  }
  
  class ProductB : IProduct {
    public String ShipFrom () {
            return "from Spain";
    }
  }

  class DefaultProduct : IProduct {
    public String ShipFrom () {
            return "not available";
    }
  }

  class Creator {
    public  IProduct FactoryMethod(int month) {
      if (month >= 4 && month <=11)
        return new ProductA();
      else 
      if (month == 1 || month == 2 || month == 12)
        return new ProductB();
      else 
        return new DefaultProduct();
    }
  }
  
    static void Main() {
      Creator c = new Creator();
      IProduct product;
        
      for (int i=1; i<=12; i++) {
        product = c.FactoryMethod(i);
        Console.WriteLine("Avocados "+product.ShipFrom());
      }
    }
  }
  
/* Output
Avocados from Spain
Avocados from Spain
Avocados not available
Avocados  from South Africa
Avocados  from South Africa
Avocados  from South Africa
Avocados  from South Africa
Avocados  from South Africa
Avocados  from South Africa
Avocados  from South Africa
Avocados  from South Africa
Avocados from Spain
*/
  

  
