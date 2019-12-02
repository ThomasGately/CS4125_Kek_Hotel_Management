using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static CS4125_Kek_Hotel_Management.Payment.DateDiscountObserver;

namespace CS4125_Kek_Hotel_Management.Payment
{
    /// <summary>
    /// The Subject abstract class
    /// </summary>
    public abstract class Observer
    {
        protected Subject subject;
        public abstract void update();
    }

    public class DateDiscountObserver : Observer
    {
        public DateDiscountObserver(Subject subject)
        {
            this.subject = subject;
            this.subject.attach(this);
        }

        public override void update()
        {
            String DiscountD = "21/07/2020";    //For this implementation we hardcoded a date.
            if (DateTime.Now.ToString("MM/dd/yyyy") == DiscountD)
            {
                Console.WriteLine("Fathers Day Discount");
            }
        }

        public class Subject
        {
            private List<Observer> observers = new List<Observer>();
            private DateTime state;


            public void setState(DateTime dateTime)
            {
                this.state = dateTime;
                notifyAllObservers();
            }

            public void attach(Observer observer)
            {
                observers.Add(observer);
            }

            public void notifyAllObservers()
            {
                foreach (Observer observer in observers)
                {
                    observer.update();
                }
            }
        }
    }
}