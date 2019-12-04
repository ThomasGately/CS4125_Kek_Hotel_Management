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
        public abstract void Update();
    }

    public class DateDiscountObserver : Observer
    {
        public DateDiscountObserver(Subject subject)
        {
            this.subject = subject;
            this.subject.Attach(this);
        }

        public override void Update()
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


            public void SetState(DateTime dateTime)
            {
                this.state = dateTime;
                NotifyAllObservers();
            }

            public void Attach(Observer observer)
            {
                observers.Add(observer);
            }

            public void NotifyAllObservers()
            {
                foreach (Observer observer in observers)
                {
                    observer.Update();
                }
            }
        }
    }
}