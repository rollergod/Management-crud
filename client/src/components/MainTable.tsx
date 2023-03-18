import axios from "axios";
import React from "react";
import Table from 'react-bootstrap/Table';
import { client } from "../api/axiosInstance";
import OrderForm from "./OrderForm";

interface Item {
    orderId: number,
    name: string,
    quantity: number,
    unit: string
}

interface Order {
    id: number,
    number: number,
    date: string,
    items: Item[]
}

const MainTable = () => {

    const [orders, setOrders] = React.useState<Order[]>([]);
    const [form, setForm] = React.useState<boolean>(false);
    const headers = [
        'Id',
        'Number',
        'Date',
        'OrderId',
        'Name',
        'Quantity',
        'Unit'
    ];

    React.useEffect(() => {
        axios.get("https://localhost:7212/api/order")
            .then((response) => {
                console.log('test', response.data);
                setOrders(response.data);
                console.log(orders)
            })
            .catch((error) => {
                console.log('error', error);
            })
    }, []);

    return (
        <div>
            <button onClick={() => setForm((prev) => !prev)} className='btn btn-primary'>Add New Order</button>
            {
                form && <OrderForm open={form} setOpen={setForm}></OrderForm>
            }
            <Table responsive>
                <thead>
                    <tr>
                        {
                            headers.map((header, index) => (
                                <th key={index}>{header}</th>
                            ))
                        }
                    </tr>
                </thead>
                <tbody>
                    {

                        orders.map((order, index) => {
                            return (
                                <tr key={index}>
                                    <td>{order.id}</td>
                                    <td>{order.number}</td>
                                    <td>{order.date}</td>
                                    <td>{order.items[0].orderId}</td>
                                    <td>{order.items[0].name}</td>
                                    <td>{order.items[0].quantity}</td>
                                    <td>{order.items[0].unit}</td>
                                </tr>
                            )
                        })

                    }
                </tbody>
            </Table>
        </div>
    );
}

export default MainTable;