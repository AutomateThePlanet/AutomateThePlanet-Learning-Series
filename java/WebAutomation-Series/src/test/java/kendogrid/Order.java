package kendogrid;

import java.time.LocalDateTime;
import java.util.UUID;
import java.util.concurrent.ThreadLocalRandom;

public class Order {
    private int orderId;
    private String shipName;
    private double freight;
    private LocalDateTime orderDate;

    public Order(String shipName) {
        this.shipName = shipName;
    }

    public Order() {
        this(UUID.randomUUID().toString());
        orderId = ThreadLocalRandom.current().nextInt(); ;
        freight = ThreadLocalRandom.current().nextDouble();
        orderDate = LocalDateTime.now();
    }

    public String getOrderId() {
        return Integer.toString(orderId);
    }

    public void setOrderId(int orderId) {
        this.orderId = orderId;
    }

    public String getShipName() {
        return shipName;
    }

    public void setShipName(String shipName) {
        this.shipName = shipName;
    }

    public double getFreight() {
        return freight;
    }

    public void setFreight(double freight) {
        this.freight = freight;
    }

    public LocalDateTime getOrderDate() {
        return orderDate;
    }

    public void setOrderDate(LocalDateTime orderDate) {
        this.orderDate = orderDate;
    }
}
