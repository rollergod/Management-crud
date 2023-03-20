import axios from "axios";
import React from "react";
import Table from 'react-bootstrap/Table';
import { client } from "../api/axiosInstance";
import OrderForm from "./OrderForm";

// interface Item {
//     orderId: number,
//     name: string,
//     quantity: number,
//     unit: string
// }

interface Order {
    id: number,
    number: number,
    date: string,
}

const MainTable = () => {

    const [orders, setOrders] = React.useState<Order[]>([]);
    const [editForm, setEditForm] = React.useState<boolean>(false);
    const [addForm, setAddForm] = React.useState<boolean>(false);
    const [orderId, setOrderId] = React.useState<number>(0);
    const headers = [
        'Id',
        'Number',
        'Date',
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

    const rowClickHandler = (id: number) => {
        console.log(id);
        setOrderId(id);
        setEditForm((prev) => !prev);
    }

    return (
        <div>
            <button onClick={() => setAddForm((prev) => !prev)} className='btn btn-primary'>Add New Order</button>
            {
                addForm && <OrderForm headerText='Добавить заказ' open={addForm} setOpen={setAddForm}></OrderForm>
                || editForm && <OrderForm orderId={orderId} headerText='Изменить заказ' open={editForm} setOpen={setEditForm}></OrderForm>
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
                                <tr onClick={() => rowClickHandler(order.id)} key={index}>
                                    <td>{order.id}</td>
                                    <td>{order.number}</td>
                                    <td>{order.date}</td>
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