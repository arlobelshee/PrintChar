using System;
using System.Linq;
using System.Windows.Documents;
using JetBrains.Annotations;
using WotcOnlineDataRepository;

namespace PrintChar
{
	public static class DocumentExtensions
	{
		[NotNull]
		public static FlowDocument ToDocument(this WotcOnlineDataRepository.Power power,
			[NotNull] Action<FlowDocument> formatTheDocument, [NotNull] Action<Paragraph> formatEachParagraph)
		{
			var result = new FlowDocument();
			formatTheDocument(result);
			if (power != null)
				result.Blocks.AddRange(power.Description.Select(d => d.ToParagraph(formatEachParagraph)));
			return result;
		}

		[NotNull]
		public static Paragraph ToParagraph([NotNull] this Descriptor d, [NotNull] Action<Paragraph> formatEachParagraph)
		{
			var label = new Bold(new Run(d.Label));
			var hasBothLabelAndDescription = !(String.IsNullOrEmpty(d.Label) || String.IsNullOrEmpty(d.Details));
			if (hasBothLabelAndDescription)
				label.Inlines.Add(new Run(": "));
			var result = new Paragraph();
			formatEachParagraph(result);
			result.Inlines.Add(label);
			result.Inlines.Add(new Run(d.Details));
			return result;
		}
	}
}
