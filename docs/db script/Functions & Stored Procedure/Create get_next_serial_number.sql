CREATE FUNCTION "public"."get_next_serial_number"() 
RETURNS INTEGER
AS
$$
DECLARE
    next_sn INTEGER;
BEGIN
    SELECT MAX(serialNumber) INTO next_sn
    FROM products_tb;

    IF next_sn IS NULL THEN
        RETURN 1;
    ELSE
        RETURN next_sn + 1;
    END IF;
END;
$$ LANGUAGE plpgsql