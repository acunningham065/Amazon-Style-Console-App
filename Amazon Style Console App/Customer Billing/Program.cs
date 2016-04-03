using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer_Billing
{
    class Program
    {
        static void Main(string[] args)
        {

            /*
             * Algorithm
             * while choice != 9
                 * 1. Display Choices
                 * 2. Collect and test choice

             * if choice = 9 
                 * 3. Calculate Total Exc. VAT
                 * 4. Calculate Total VAT
                 * 5. Calculate Total Inc. VAT
                 * 6. Output calculations in bill format
                 * 7. Print payment methods
                 * 8. Collect and test payment method
                 * 9. Display appropriate output
             */

            //Declaration Of Variables            
            string[] products = new string[] //excellent use of string array
            { 
                "AMD Processor", 
                "Intel Processor", 
                "NVIDIA Graphics Card", 
                "AMD Graphics Card", 
                "Power Supply", 
                "8GB of RAM", 
                "16GB of RAM", 
                "Optical Disk Drive", 
                "Proceed to checkout" 
            };

            int[] prices = new int[] //commment
            { 
                100, 
                200, 
                150, 
                150, 
                100, 
                75, 
                125, 
                20
            };

            string[] productsSelected = new string[1];  //use comments!
            int[] pricesOfSelected = new int[1]{0};
            int productNumber = 1;
            int totalBasket = 0;
            int productSelectedIdentifier = 0;
            int totalExcVat = 0;
            double totalVat = 0;
            double vatRate = 0.20;
            double totalIncVat = 0;
            string name = "";
            
            


            //----------------------------------------------End Variable Declaration----------------------------------------------



            //Welcome
            Console.WriteLine("Welcome to PC Zone (not a rip-off of PC World at all)");
            
            BlankLine();


            while (name == "")
            {
                Console.WriteLine("What is your name?");

                BlankLine();

                name = Console.ReadLine();

                BlankLine();

            }//end while


            //1. Display Choices and collect choices until choices are done
            while (productSelectedIdentifier != 8)  ///8 is the Quit option!
            {
                
                //Recieve selected product after displaying menu
                productSelectedIdentifier = MainMenuAndChoiceHandler(products, prices, productsSelected, productNumber, pricesOfSelected, totalBasket);


                if (productSelectedIdentifier == 8)
                {

                    //This exits the while loop and prints the itemised reciept
                    break;

                }
                else
                {

                    //Insert product selected for itemised reciept
                    productsSelected[productNumber - 1] = products[productSelectedIdentifier];                    

                    //Insert price of product selected for itemised reciept and final calculation
                    pricesOfSelected[productNumber - 1] = prices[productSelectedIdentifier];

                    //Increase size off array by one
                    Array.Resize(ref productsSelected, productsSelected.Length + 1);
                    Array.Resize(ref pricesOfSelected, pricesOfSelected.Length + 1);

                }//End if
                

                //increment productNumber
                productNumber = productNumber + 1;



            }//End While for Collecting choices


            BlankLine();


            //Pause and explain the coming clear
            Console.WriteLine("Press any key to generate your bill");
            Console.ReadKey();
            Console.Clear();
           

            //Generates Bill
            GenerateItemisedReciept(productsSelected, pricesOfSelected, totalExcVat, vatRate, totalVat, totalIncVat, name);

            Console.WriteLine("__________________________________________________________");


            PaymentMethod();


            //Pause
            Console.ReadKey();
            
        }//End Main

        

        private static int MainMenuAndChoiceHandler(string[] products, int[] prices, string[] productsSelected, int productNumber, int[] pricesOfSelected, int totalBasket)
        {
            //Declaration Of Variables
            string userMenuChoiceString;
            int userMenuChoiceInt;
            bool validChoice = false;
            int productId = 0;
            string selectedProduct;
            int priceOfSelectedProduct;

            //Display the menu
            DisplayMenu(products, productsSelected, pricesOfSelected, totalBasket);

            BlankLine();

            Console.WriteLine("Selection " + productNumber);

            BlankLine();

            while (validChoice == false)
            {

                //read choice in
                userMenuChoiceString = Console.ReadLine();

                
                //Check for valid int
                userMenuChoiceInt = ValidIntEntered("Please enter a valid number", userMenuChoiceString);
                

                //Check for a valid choice
                if (userMenuChoiceInt >= 1 && userMenuChoiceInt <= 8)
                {

                    //Assign products for testing and efficeint passing
                    productId = userMenuChoiceInt - 1;
                    selectedProduct = products[productId];
                    priceOfSelectedProduct = prices[productId];

                    //A valid choice has been made so finish selection
                    ValidChoiceMade(selectedProduct, priceOfSelectedProduct);

                    validChoice = true;

                }
                else if (userMenuChoiceInt == 9)
                {

                    return productId = 8;

                }
                else
                {
                    BlankLine();

                    //Invalid choice made so inform of error and clear screen
                    Console.WriteLine("You have entered an incorrect choice \nTry Again");

                    BlankLine();

                }//End Valid choice if

            }//End While notValidChoice loop

            return productId;

        }//End MainMenuAndChoiceHandler



        private static void DisplayMenu(string[] products, string[] productsSelected, int[] pricesOfSelected, int totalBasket)
        {

            //Print menu
            Console.WriteLine("Please select (numerically) the product you wish to purchase: ");

            BlankLine();

            //Display menu
            for (int arrayIndicator = 0; arrayIndicator < products.Length; arrayIndicator++)
            {

                Console.WriteLine((arrayIndicator + 1) + ". " + products[arrayIndicator]);

            }//End for


            BlankLine();


            Console.WriteLine("__________________________________________________________\n");


            Console.WriteLine("Basket: \n");


            //Display Basket
            for (int arrayIndicatorBasket = 0; arrayIndicatorBasket < (productsSelected.Length - 1); arrayIndicatorBasket++)
            {

                if (productsSelected[arrayIndicatorBasket].Length < 17)
                {

                    Console.WriteLine(productsSelected[arrayIndicatorBasket] + "\t\t" + pricesOfSelected[arrayIndicatorBasket].ToString("C"));

                }
                else
                {

                    Console.WriteLine(productsSelected[arrayIndicatorBasket] + "\t" + pricesOfSelected[arrayIndicatorBasket].ToString("C"));

                }//End alignment If


                totalBasket = totalBasket + pricesOfSelected[arrayIndicatorBasket];


            }//End For


            Console.WriteLine("__________________________________________________________");


            BlankLine();


            Console.WriteLine("Total in basket = \t" + totalBasket.ToString("C"));

        }//End DisplayMenu



        private static void ValidChoiceMade(string selectedProduct, int priceOfSelectedProduct)
        {
            //Print out product info
            BlankLine();

            Console.WriteLine("You have Selected " + selectedProduct + " which costs " + priceOfSelectedProduct.ToString("C") + " \n\nThe menu will now reappear with the new entries\nPress any key to continue");

            BlankLine();

            //Pause
            Console.ReadKey();

            //Clear
            Console.Clear();

        }//End ValidCoiceMade


       
        private static int ValidIntEntered(string question, string enteredString)
        {
            //Declare Variables
            bool validInt = false;
            int enteredNumber = 0;

            //Check for valid int, repeat question until valid int is given
            while (validInt == false)
            {

                //Try converting to int
                validInt = Int32.TryParse(enteredString, out enteredNumber);

                if (validInt == false)
                {

                    BlankLine();

                    //Conversion false ask again
                    Console.WriteLine(question);

                    BlankLine();

                    enteredString = Console.ReadLine();

                    BlankLine();

                }//End If

            }//End While

            return enteredNumber;

        }//End ValidIntEntered



        private static void GenerateItemisedReciept(string[] productsSelected, int[] pricesOfSelected, int totalExcVat, double vatRate, double totalVat, double totalIncVat, string name)
        {
            //Declaration of Variables
            Random rnd = new Random();

            //General Info for reciept
            Console.WriteLine(DateTime.Now);

            BlankLine();

            Console.WriteLine("Customer Number: " + rnd.Next(100, 1000).ToString());

            BlankLine();

            Console.WriteLine("Customer Name: " + name);

            BlankLine();

            Console.WriteLine("Item:\t\t\tPrice:");

            BlankLine();


            //Print itemised reciept
            for (int arrayIndicator = 0; arrayIndicator < (productsSelected.Length - 1); arrayIndicator++)
            {

                //Test length to see if it requires one or two tabs for alignment
                if (productsSelected[arrayIndicator].Length < 17)
                {

                    Console.WriteLine(productsSelected[arrayIndicator] + "\t\t" + pricesOfSelected[arrayIndicator].ToString("C"));

                }
                else
                {

                    Console.WriteLine(productsSelected[arrayIndicator] + "\t" + pricesOfSelected[arrayIndicator].ToString("C"));

                }//End alignment If

            }//End itemised reciept for
            

            //Calculate:

            //Total Exc. VAT
            for (int arrayIndicator = 0; arrayIndicator < (pricesOfSelected.Length - 1); arrayIndicator++)
            {
                totalExcVat = totalExcVat + pricesOfSelected[arrayIndicator];
            }

            //Total VAT
            totalVat = totalExcVat * vatRate;

            //Total Inc VAT
            totalIncVat = totalExcVat + totalVat;

            //Display Reciept
            PrintCalculations(totalExcVat, totalVat, totalIncVat);

        }//End GenerateItemisedReciept



        private static void PrintCalculations(int totalExcVat, double totalVat, double totalIncVat)
        {
            BlankLine();

            Console.WriteLine("Totals:");

            Console.WriteLine("\nTotal exc. VAT = " + totalExcVat.ToString("C"));

            Console.WriteLine("\nTotal VAT at 20% = " + totalVat.ToString("C"));

            Console.WriteLine("\nTotal inc. VAT = " + totalIncVat.ToString("C"));

        }//End PrintCalculations



        private static void PaymentMethod()
        {
            //Declaration Of Variables
            bool paymentMethodValid = false;
            bool validCardNumber = false;
            bool validEmail = false;
            bool validDate = false; ;
            int paymentMethodSelectedInt = 0;
            int expiryMonth = 0;
            int expiryYear = 0;
            int currentMonth = 10;
            int currentYearShort = 14;
            int currentYearLong = 2014;
            string paymentMethodSelectedString;
            string[] paymentMethods = new string[]
            {
                "Credit Card",
                "Debit Card",
                "PayPal"
            };

            BlankLine();

            //Ask which payment method
            Console.WriteLine("How do you wish to pay?");

            //Write array out
            for (int arrayIndicator = 0; arrayIndicator < paymentMethods.Length; arrayIndicator++)
            {
                Console.WriteLine((arrayIndicator + 1).ToString() + ". " + paymentMethods[arrayIndicator]);
            }

            BlankLine();

            Console.WriteLine("Please choose numerically\n");

            //while they haven't chosen correctly
            while (paymentMethodValid == false)
            {

                //Read line
                paymentMethodSelectedString = Console.ReadLine();


                //Check valid int
                paymentMethodSelectedInt = ValidIntEntered("Please enter a valid number", paymentMethodSelectedString);


                //Check if in range
                if (paymentMethodSelectedInt >= 1 && paymentMethodSelectedInt <= paymentMethods.Length)
                {

                    paymentMethodValid = true;

                }
                else
                {

                    BlankLine();
                    Console.WriteLine("Please enter a valid choice");
                    BlankLine();
                    paymentMethodValid = false;

                }//End range if

            }//End paymentValid while


            //According to selected payment method
            switch (paymentMethodSelectedInt)
            {
                case 1:

                    CheckCardNumber(ref validCardNumber);

                    while (validDate == false)
                    {

                        //Check if Valid month
                        expiryMonth = ValidExpiryMonth();


                        //Check if Valid year
                        expiryYear = ValidExpiryYear();


                        //Check to see if the date given is valid for month and year together
                        if (expiryYear == currentYearShort | expiryYear == currentYearLong)
                        {

                            if (expiryMonth <= currentMonth)
                            {

                                //Invalid
                                Console.WriteLine("You have entered an invalid date");

                                validDate = false;

                            }
                            else
                            {

                                //Valid
                                validDate = true;

                            }//End Month if

                        }
                        else
                        {

                            //Valid
                            validDate = true;

                        }//End Range if
                        

                    }//End While                   
                    

                    Console.WriteLine("Thank You for shopping with PC Zone");


                    break;

                case 2:

                    CheckCardNumber(ref validCardNumber);

                    while (validDate == false)
                    {

                        //Check if Valid month
                        expiryMonth = ValidExpiryMonth();


                        //Check if Valid year
                        expiryYear = ValidExpiryYear();


                        //Check to see if the date given is valid for month and year together
                        if (expiryYear == currentYearShort | expiryYear == currentYearLong)
                        {

                            if (expiryMonth <= currentMonth)
                            {
                                //Invalid
                                Console.WriteLine("You have entered an invalid date");

                                BlankLine();

                                validDate = false;
                                continue;

                            }
                            else
                            {

                                //Valid
                                validDate = true;

                            }//End Month if
                        }
                        else
                        {

                            //Valid
                            validDate = true;

                        }//End Range if
                        
                    }//End While

                    Console.WriteLine("Thank You for shopping with PC Zone");

                    break;

                case 3:

                    ValidEmail(ref validEmail);

                    Console.WriteLine("Thank You for shopping with PC Zone");

                    break;

                default:

                    Console.WriteLine("Incorrect method chosen. Aborting Sale...");

                    break;

            }//End Switch

        }//End PaymentMethod



        private static void ValidEmail(ref bool validEmail)
        {
            //Declaration of Variables
            string email;


            while (validEmail == false)
            {
                //Ask
                Console.WriteLine("Please enter your Email address\n");

                //Collect
                email = Console.ReadLine();

                //Check length
                if (email.Length >= 11)
                {
                    //valid
                    validEmail = true;

                }
                else
                {
                    //Invalid
                    Console.WriteLine("A valid email should be at least 11 characters");
                    validEmail = false;
                    continue;

                }//End length if

                //for comparison/testing
                string endOfEmailAddress = email.Substring(email.Length - 4);

                //test if the end is .com
                if (endOfEmailAddress != ".com")
                {
                    //Valid
                    Console.WriteLine("Valid Email addresses end in .com");
                    validEmail = false;
                    continue;

                }
                else
                {
                    //Invalid
                    validEmail = true;

                }//End .com if

                BlankLine();

            }//End while


        }//End ValidEmail


               
        private static void CheckCardNumber(ref bool validCardNumber)
        {
            string cardNumber;

            while (validCardNumber == false)
            {
                BlankLine();

                //Ask
                Console.WriteLine("Please enter your 12 digit card number beneath\n");

                //Collect
                cardNumber = Console.ReadLine();

                //Check Length
                if (cardNumber.Length != 12)
                {

                    validCardNumber = false;
                    continue;

                }
                else
                {

                    validCardNumber = true;

                }//End length if


                //Check if digit
                foreach (char individualCharacter in cardNumber)
                {

                    if (char.IsDigit(individualCharacter))
                    {

                        validCardNumber = true;

                    }
                    else
                    {

                        validCardNumber = false;
                        break;

                    }//End digit check

                }//End foreach


                BlankLine();


            }//End Card Number while


        }//End CheckCardNumber



        private static int ValidExpiryMonth()
        {
            int expiryMonthInt = 0;
            string expiryMonth;
            bool validExpiryMonth = false;

            while (validExpiryMonth == false)
            {
                //Ask
                Console.WriteLine("Please enter the month of expiry\n");

                //Collect
                expiryMonth = Console.ReadLine();

                //Check only 2 digits
                if (expiryMonth.Length != 2)
                {
                    //if not 2 digits
                    validExpiryMonth = false;
                    continue;

                }
                else
                {
                    //if 2 digits
                    validExpiryMonth = true;

                }//End length check


                //Check if and convert to int
                expiryMonthInt = ValidIntEntered("Please enter a valid date", expiryMonth);


                //Check if between Jan and Dec
                if (expiryMonthInt >= 1 && expiryMonthInt <= 12)
                {
                    //if between Jan and Dec
                    validExpiryMonth = true;

                }
                else
                {
                    //if between Jan and Dec
                    validExpiryMonth = false;
                    continue;

                }//End range check

                BlankLine();

            }//End while

            return expiryMonthInt;

        }//End ValidExpiryMonth



        private static int ValidExpiryYear()
        {
            //Declaration of Variables
            int expiryYearInt = 0;
            int currentYearLong = 2014;
            string expiryYear;
            bool validExpiryYear = false;

            while (validExpiryYear == false)
            {
                //Ask
                Console.WriteLine("Please enter the year of expiry in full (for example 2014)\n");

                //Collect
                expiryYear = Console.ReadLine();

                //Check only 4 digits
                if (expiryYear.Length != 4)
                {
                    //if not 4 digits
                    validExpiryYear = false;
                    continue;

                }
                else
                {
                    //if 4 digits
                    validExpiryYear = true;

                }//End length check


                //Check if and convert to int
                expiryYearInt = ValidIntEntered("Please enter a valid date", expiryYear);


                //Check not previous years
                if (expiryYearInt >= currentYearLong)
                {
                    //If not previous years continue
                    validExpiryYear = true;

                }
                else
                {
                    //Invalid year entered
                    validExpiryYear = false;
                    continue;

                }//End If

                BlankLine();

            }//End While

            return expiryYearInt;


        }//End ValidExpiryYear



        private static void BlankLine()
        {

            Console.WriteLine();

        }//End BlankLine

    }
}
