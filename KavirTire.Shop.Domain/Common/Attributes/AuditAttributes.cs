namespace KavirTire.Shop.Domain.Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public sealed class AuditableAttribute : Attribute
{ }

[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public sealed class NotAuditableAttribute : Attribute
{ }