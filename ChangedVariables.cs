using System.ComponentModel;
namespace org.SpocWeb.root.logging.Tests;

/// <summary> Data container holding field and value names used as structured-log test fixtures. </summary>
/// <remarks>
/// ## Meta
/// pass: 2
/// mtime: 2026-05-15T20:55:22Z
/// digest: a62af09d34a7fc8fcd1bcf3fc2f287a193777ead6e78c00f1979c3df92fa8316
/// updated: 2026-05-19
/// </remarks>
/// <example>
/// <code language="yaml">
/// pass: 2
/// mtime: 2026-08-22T17:19:37Z
/// digest: 77d93cf72221d129132dc1a0eb8fd8c3f8c8cd462302625a599ce8d7ae60fdb2
/// </code>
/// </example>
public class ChangedVariables
{
    /// <summary>Gets or sets the product Mask Field.</summary>
    [System.ComponentModel.Description("Gets or sets the product Mask Field.")]
    public string ProductMaskField { get; set; }

    /// <summary>Gets or sets the product Mask Value.</summary>
    [System.ComponentModel.Description("Gets or sets the product Mask Value.")]
    public string ProductMaskValue { get; set; }

    /// <summary>Gets or sets the money Value.</summary>
    [System.ComponentModel.Description("Gets or sets the money Value.")]
    public string MoneyValue { get; set; }
}
