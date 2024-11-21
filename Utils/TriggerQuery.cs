public class TriggerQuery
{
    #region After-Insert
    public static string AfterProductsInsert()
    {
        return @"
        CREATE TRIGGER after_products_insert
        AFTER INSERT ON products
        FOR EACH ROW
        BEGIN
            UPDATE product_categories
            SET Total_Product = Total_Product + NEW.Quantity
            WHERE Id = NEW.CategoryId;
        END;";
    }

    public static string AfterProductCategoriesInsert()
    {
        return @"
        CREATE TRIGGER after_product_categories_insert
        AFTER INSERT ON product_categories
        FOR EACH ROW
        BEGIN
            UPDATE product_main_categories
            SET Total_Product = Total_Product + NEW.Total_Product
            WHERE Id = NEW.Main_CategoryId;
        END;";
    }

    public static string AfterItemOrderInsert()
    {
        return @"
        CREATE TRIGGER after_item_orders_insert
        AFTER INSERT ON item_orders
        FOR EACH ROW
        BEGIN
            DECLARE checkPrice DECIMAL(10, 2);
            SELECT Price INTO checkPrice FROM products WHERE Id = NEW.ProductId;

            UPDATE sub_orders
            SET Total_Price = Total_Price + (checkPrice * NEW.Quantity)
            WHERE Id = NEW.Sub_OrderId;
        END;";
    }

    public static string AfterSubOrderInsert()
    {
        return @"
        CREATE TRIGGER after_sub_orders_insert
        AFTER INSERT ON sub_orders
        FOR EACH ROW
        BEGIN
            UPDATE orders
            SET Total_Price = Total_Price + NEW.Total_Price
            WHERE Id = NEW.OrderId;
        END;";
    }

    public static string AfterFeedbacksInsert()
    {
        return @"
        CREATE TRIGGER after_feedbacks_insert
        AFTER INSERT ON feedbacks
        FOR EACH ROW
        BEGIN
            UPDATE products
            SET 
                Total_Rating_Quantity = Total_Rating_Quantity + 1, 
                Total_Rating_Value = Total_Rating_Value + NEW.Rating
            WHERE Id = NEW.ProductId;

            UPDATE item_orders
            SET IsRating = true
            WHERE Id = NEW.Item_OrderId; 
        END;";
    }
    #endregion

    #region After-Update
    public static string AfterProductsUpdate()
    {
        return @"
        CREATE TRIGGER after_products_update
        AFTER UPDATE ON products
        FOR EACH ROW
        BEGIN
            DECLARE quantityDiff INT;
            IF NEW.Quantity <> OLD.Quantity THEN
                SET quantityDiff = NEW.Quantity - OLD.Quantity;
                UPDATE product_categories
                SET Total_Product = Total_Product + quantityDiff
                WHERE Id = NEW.CategoryId;
            END IF;
        END;";
    }

    public static string AfterProductCategoriesUpdate()
    {
        return @"
        CREATE TRIGGER after_product_categories_update
        AFTER UPDATE ON product_categories
        FOR EACH ROW
        BEGIN
            DECLARE quantityDiff INT;
            IF NEW.Total_Product <> OLD.Total_Product THEN
                SET quantityDiff = NEW.Total_Product - OLD.Total_Product;
                UPDATE product_main_categories
                SET Total_Product = Total_Product + quantityDiff
                WHERE Id = NEW.Main_CategoryId;
            END IF;
        END;";
    }

    public static string AfterItemOrdersUpdate()
    {
        return @"
        CREATE TRIGGER after_item_orders_update
        AFTER UPDATE ON item_orders
        FOR EACH ROW
        BEGIN
            DECLARE quantityDiff INT;
            DECLARE checkPrice DECIMAL(10, 2);
            SELECT Price INTO checkPrice FROM products WHERE Id = NEW.ProductId;
            
            IF NEW.Quantity <> OLD.Quantity THEN
                SET quantityDiff = NEW.Quantity - OLD.Quantity;
                UPDATE sub_orders
                SET Total_Price = Total_Price + (checkPrice * quantityDiff)
                WHERE Id = NEW.Sub_OrderId;
            END IF;
        END;";
    }

    public static string AfterSubOrdersUpdate()
    {
        return @"
        CREATE TRIGGER after_sub_orders_update
        AFTER UPDATE ON sub_orders
        FOR EACH ROW
        BEGIN
            DECLARE priceDiff DECIMAL(10, 2);
            IF NEW.Total_Price <> OLD.Total_Price THEN
                SET priceDiff = NEW.Total_Price - OLD.Total_Price;
                UPDATE orders
                SET Total_Price = Total_Price + priceDiff
                WHERE Id = NEW.OrderId;
            END IF;
        END;";
    }

    public static string AfterFeedbacksUpdate()
    {
        return @"
        CREATE TRIGGER after_feedbacks_update
        AFTER UPDATE ON feedbacks
        FOR EACH ROW
        BEGIN
            DECLARE ratingDiff INT;
            IF NEW.Rating <> OLD.Rating THEN
                SET ratingDiff = NEW.Rating - OLD.Rating;
                UPDATE products
                SET Total_Rating_Value = Total_Rating_Value + ratingDiff
                WHERE Id = NEW.ProductId;
            END IF;
        END;";
    }
    #endregion
}
