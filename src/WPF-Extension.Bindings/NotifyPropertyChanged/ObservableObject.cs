using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace WPF_Extension.Bindings.NotifyPropertyChanged
{
    public class ObservableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;


        protected void NotifyPropertyChanging([CallerMemberName] string name = null)
           => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(name));

        protected void NotifyPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        protected virtual bool Set<T>(T value, ref T member, [CallerMemberName] string name = null)
        {
            if (EqualityComparer<T>.Default.Equals(member, value))
                return false;

            // Notify before update
            NotifyPropertyChanging(name);

            // Update
            member = value;

            // Notify after update
            NotifyPropertyChanged(name);

            return true;
        }
    }
}
