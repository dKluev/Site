/*using System.Collections;
using FluentValidation.Internal;

namespace FluentValidation.xValIntegration
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Validators;
    using xVal.RuleProviders;
    using System.Linq;
    using xVal.Rules;

    public class FluentValidationRulesProvider : CachingRulesProvider
    {

        private IValidatorFactory factory;
        private static readonly MethodInfo genericMethod;
        private readonly RuleEmitterList<IPropertyValidator> ruleEmitters = new RuleEmitterList<IPropertyValidator>();

        static FluentValidationRulesProvider()
        {
            genericMethod = typeof(FluentValidationRulesProvider).GetMethod("GetRulesFromType", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.InvokeMethod);
        }

        public FluentValidationRulesProvider(IValidatorFactory factory)
        {
            this.factory = factory;

            ruleEmitters.AddSingle<INotNullValidator>(x => new RequiredRule());
            ruleEmitters.AddSingle<INotEmptyValidator>(x => new RequiredRule());
            ruleEmitters.AddSingle<ILengthValidator>(x => new StringLengthRule(x.Min, x.Max));
            ruleEmitters.AddSingle<IRegularExpressionValidator>(x => new RegularExpressionRule(x.Expression));
            ruleEmitters.AddSingle<IComparisonValidator>(x =>
            {
                if (x.Comparison == Comparison.Equal && x.MemberToCompare != null)
                    return new ComparisonRule(x.MemberToCompare.Name, ComparisonRule.Operator.Equals);
                if (x.Comparison == Comparison.NotEqual && x.MemberToCompare != null)
                    return new ComparisonRule(x.MemberToCompare.Name, ComparisonRule.Operator.DoesNotEqual);
                if (x.Comparison == Comparison.GreaterThanOrEqual && x.ValueToCompare != null)
                    return GenerateComparisonRule(x.ValueToCompare, x.Comparison);
                if (x.Comparison == Comparison.LessThanOrEqual && x.ValueToCompare != null)
                    return GenerateComparisonRule(x.ValueToCompare, x.Comparison);
                return null;
            });

            //The rule for DelegatingValidator *must* be last
            ruleEmitters.AddMultiple<IDelegatingValidator>(x =>
            {
                var delegatingValidator = x as IDelegatingValidator;
                if (delegatingValidator != null)
                {
                    return ruleEmitters.EmitRules(delegatingValidator.InnerValidator);
                }
                return null;
            });

        }

        private RangeRule GenerateComparisonRule(object valueToCompare, Comparison comp)
        {
            if (comp == Comparison.GreaterThanOrEqual) {
                if (valueToCompare is decimal)
                    return new RangeRule((decimal) valueToCompare, null);
                if (valueToCompare is DateTime)
                    return new RangeRule((DateTime)valueToCompare, null);
                if (valueToCompare is int)
                    return new RangeRule((int)valueToCompare, null);
                if (valueToCompare is string)
                    return new RangeRule((string)valueToCompare, null);
            }
            else if (comp == Comparison.LessThanOrEqual)
            {
                if (valueToCompare is decimal)
                    return new RangeRule(null, (decimal)valueToCompare);
                if (valueToCompare is DateTime)
                    return new RangeRule(null, (DateTime)valueToCompare);
                if (valueToCompare is int)
                    return new RangeRule(null, (int)valueToCompare);
                if (valueToCompare is string)
                    return new RangeRule(null, (string)valueToCompare);
            }

            return null;
        }

        protected override RuleSet GetRulesFromTypeCore(Type type)
        {
            //hackery - this should be done better.
            //Invokes the GetRulesFromType<T> method.
            var method = genericMethod.MakeGenericMethod(type);
            return (RuleSet)method.Invoke(this, null);
        }

        private RuleSet GetRulesFromType<T>()
        {
            var validator = factory.GetValidator<T>() as IEnumerable<IValidationRule<T>>;

            if (validator != null)
            {
               
                var rules = from ruleBuilder in validator.OfType<ISimplePropertyRule<T>>()
                            from xValRule in ConvertToXValRules(ruleBuilder.Validator)
                            select new KeyValuePair<string, Rule>(ruleBuilder.Member.Name, xValRule);

                return new RuleSet(rules.ToLookup(x => x.Key, x => x.Value));
            }

            return RuleSet.Empty;
        }

        private IEnumerable<Rule> ConvertToXValRules(IPropertyValidator val)
        {
            return ruleEmitters.EmitRules(val);
        }

    }
}*/