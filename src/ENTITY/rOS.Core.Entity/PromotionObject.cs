using rOS.Core.Api;

namespace rOS.Core.Entity;

public abstract class PromotionObject : EntityBase, IMetaPromotion,INamed,ITitled
{
    public abstract string MetaDescription { get; set; }

    public MetaKeyword[] Keywords { get; set; } = Array.Empty<MetaKeyword>();

    IMetaKeyword[] IMetaPromotion.MetaKeywords => Keywords;

    public abstract string Name { get; set; } 

    public abstract string Title { get; set; }


    public abstract string  MetaTitle { get; set; }

    public PromotionObject() { }



}
