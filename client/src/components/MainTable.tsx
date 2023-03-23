import React from "react";
import Table from 'react-bootstrap/Table';
import OrderForm from "./OrderForm";
import Select from 'react-select';
import { DatePicker } from "antd";
import dayjs from "dayjs";
import { client } from "../api/axiosInstance";
const { RangePicker } = DatePicker;

interface Order {
    id: number,
    number: number,
    date: string,
}

const MainTable = () => {
    const endDate = dayjs();
    const startDate = endDate.subtract(1, 'month');

    const [dates, setDates] = React.useState<any[]>([new Date(startDate).toDateString(), new Date(endDate).toDateString()]);
    const [orders, setOrders] = React.useState<Order[]>([]);
    const [editForm, setEditForm] = React.useState<boolean>(false);
    const [addForm, setAddForm] = React.useState<boolean>(false);
    const [orderId, setOrderId] = React.useState<number>(0);

    const headers = [
        'Id',
        'Number',
        'Date',
    ];

    const options = [
        { value: 'Id asc', label: 'Id ↑' },
        { value: 'Number asc', label: 'Number ↑' },
        { value: 'Date asc', label: 'Date ↑' },
        { value: 'Id desc', label: 'Id ↓' },
        { value: 'Number desc', label: 'Number ↓' },
        { value: 'Date desc', label: 'Date ↓' },
    ];

    React.useEffect(() => {
        sendGetRequest();
    }, []);

    const filterData = () => {
        sendGetRequest();
    };

    const sendGetRequest = () => {
        client.get(`/order?startDate=${dates[0]}&endDate=${dates[1]}&orderBy=${filters}`)
            .then((response) => {
                setOrders(response.data);
            })
            .catch((error) => {
                console.log('error', error);
            })
    };

    const rowClickHandler = (id: number) => {
        setOrderId(id);
        setEditForm((prev) => !prev);
    };

    const [filters, setFilters] = React.useState<any>(['Id desc']);

    const getValue = () => {
        if (filters) {
            return options.filter(option => filters.indexOf(option.value) >= 0);
        } else {
            return [];
        }
    };

    const onChange = (newValue: any) => {
        setFilters(newValue.map((v: any) => v.value));
    };

    return (
        <div>
            <button onClick={() => setAddForm((prev) => !prev)} className='btn btn-primary'>Add New Order</button>
            {
                addForm && <OrderForm headerText='Добавить заказ' open={addForm} setOpen={setAddForm} />
                || editForm && <OrderForm headerText='Изменить заказ' open={editForm} setOpen={setEditForm} orderId={orderId} />
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

                <Select options={options} onChange={onChange} value={getValue()} isMulti={true}></Select>

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
                                <tr style={{ cursor: "pointer" }} onClick={() => rowClickHandler(order.id)} key={index}>
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