using rOS.Core.Api;

namespace rOS.Core.Entity;

public abstract class PromotionEntity : ContactBase, IMetaPromotion
{
    public abstract string MetaDescription { get; set; }

    public  MetaKeyword[] MetaKeywords { get; set; } = Array.Empty<MetaKeyword>();

    IMetaKeyword[] IMetaPromotion.MetaKeywords => MetaKeywords;

    public abstract string MetaTitle { get; set; }

    public PromotionEntity() { }
  

}
