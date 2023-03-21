import axios from "axios";
import React from "react";
import Table from 'react-bootstrap/Table';
import { client } from "../api/axiosInstance";
import OrderForm from "./OrderForm";

import { DatePicker } from "antd";
import moment from "moment";
import dayjs from "dayjs";
const { RangePicker } = DatePicker;

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
    const endDate = dayjs();
    const startDate = endDate.subtract(1, 'month');
    const [dates, setDates] = React.useState([new Date(startDate).toDateString(), new Date(endDate).toDateString()]);
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
        axios.get(`https://localhost:7212/api/order?startDate=${dates[0]}&endDate=${dates[1]}`)
            .then((response) => {
                console.log('test', response.data);
                setOrders(response.data);
                console.log(orders)
            })
            .catch((error) => {
                console.log('error', error);
            })
    }, []);

    const filterData = () => {
        axios.get(`https://localhost:7212/api/order?startDate=${dates[0]}&endDate=${dates[1]}`)
            .then((response) => {
                console.log('test', response.data);
                setOrders(response.data);
                console.log(orders)
            })
            .catch((error) => {
                console.log('error', error);
            })
    };

    const rowClickHandler = (id: number) => {
        setOrderId(id);
        setEditForm((prev) => !prev);
    }


    console.log('end date', dates[0]);

    return (
        <div>
            <button onClick={() => setAddForm((prev) => !prev)} className='btn btn-primary'>Add New Order</button>
            {
                addForm && <OrderForm headerText='Добавить заказ' open={addForm} setOpen={setAddForm}></OrderForm>
                || editForm && <OrderForm orderId={orderId} headerText='Изменить заказ' open={editForm} setOpen={setEditForm}></OrderForm>
            }
            <div style={{ margin: 20 }}>
                <RangePicker format={'MM-DD-YYYY'} defaultValue={[startDate, endDate]} onChange={(values) => {
                    setDates(values.map(item => {
                        return new Date(item).toDateString()
                    }))
                }} />
                <button
                    style={{ marginLeft: 20 }}
                    onClick={() => filterData()}
                    className='btn btn-primary'>Filter</button>
            </div>
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
                                    <td>{dayjs(order.date).format('MM-DD-YYYY')}</td>
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