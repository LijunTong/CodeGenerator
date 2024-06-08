using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Mvvm;
using Prism.Navigation;

namespace CodeGenerator.Core.Mvvm
{
    public abstract class ViewModelBase : ObservableObject, IDestructible
    {
        protected ViewModelBase()
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
