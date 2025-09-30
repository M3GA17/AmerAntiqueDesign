using Shared.ValueObjects;
using System.Collections.ObjectModel;

namespace Shared.Primitives;

public abstract class WellKnown<T, ID, TInstance>(ID id)
    where T : notnull
    where ID : BaseId<T>
    where TInstance : WellKnown<T, ID, TInstance>
{
    public ID Id { get; } = id;

    #region methods
    public virtual bool In(params TInstance[] values) => values.Any(s => s.Id == Id);
    public virtual bool NotIn(params TInstance[] values) => !In(values);
    public static ReadOnlyCollection<TInstance> All =>
        new(typeof(TInstance).GetFields()
            .Where(members => members.FieldType == typeof(TInstance))
            .Select(members => (TInstance)members.GetValue(typeof(TInstance).GetField(members.Name))!)
            .ToList());

    public static TInstance GetById(ID id) => All.SingleOrDefault(x => x.Id == id)
        ?? throw new ArgumentOutOfRangeException(nameof(id), id.Value, string.Empty);
    #endregion methods
}
