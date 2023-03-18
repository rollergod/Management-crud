import React from "react";
import Table from 'react-bootstrap/Table';
import { client } from "../api/axiosInstance";

const MainTable = () => {

    const [orders, setOrders] = React.useState([]);

    const headers = [
        'Id',
        'Number',
        'Date',
        'OrderId',
        'Name',
        'Quantity',
        'Unit'
    ];

    const fakeData = [
        {
            Id: 1,
            Number: 'Test123',
            Date: '2011-01-01',
            OrderId: 1,
            Name: 'Груша',
            Quantity: 15,
            Unit: 'Единиц'
        }
    ];

    React.useEffect(() => {
        client.get("/order")
            .then((response) => {
                console.log(response);
                setOrders(response.data);
            })
            .catch((error) => {
                console.log(error);
            })
    }, []);

    return (
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

                    fakeData.map((item, index) => {
                        return (
                            <tr key={index}>
                                <td>{item.Id}</td>
                                <td>{item.Number}</td>
                                <td>{item.Date}</td>
                                <td>{item.OrderId}</td>
                                <td>{item.Name}</td>
                                <td>{item.Quantity}</td>
                                <td>{item.Unit}</td>
                            </tr>
                        )
                    })

                }
            </tbody>
        </Table>
    );
}

export default MainTable;