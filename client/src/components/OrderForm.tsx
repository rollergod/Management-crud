import React from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

import Select from 'react-select';
import axios from 'axios';

const OrderForm = ({ open, setOpen }) => {


    const handleClose = () => setOpen(false);
    const [number, setNumber] = React.useState('');
    const [providerId, setProviderId] = React.useState(0);
    const [rows, setRows] = React.useState([{
        name: '', quantity: '', unit: ''
    }]);

    const providers = [
        {
            value: '1', label: 'TestProvider1'
        },
        {
            value: '2', label: 'TestProvider2'
        }
    ]; // TODO : доделать заказчиков

    const handleSubmit = () => {
        console.log(rows);

        const data = {
            'number': number,
            'providerId': providerId,
            'items': rows
        };

        console.log(data);

        // axios.post("https://localhost:7212/api/order",)
    }

    const handleFormChange = (index, event) => {
        let data = [...rows];
        data[index][event.target.name] = event.target.value;
        setRows(data);
    };

    const addRow = () => {
        let newRow = { name: '', quantity: '', unit: '' }

        setRows([...rows, newRow]);
    }

    const removeFields = (index: number) => {
        let data = [...rows];
        data.splice(index, 1)
        setRows(data)
    }

    const handleSelectChange = (option) => {
        setProviderId(option.value);
    }

    return (
        <Modal show={open} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Modify Employee</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Row>
                    <Col>
                        <input type="text" className='form-control' placeholder='Введите номер заказа' value={number} onChange={(e) => setNumber(e.target.value)} />
                    </Col>
                    <Col>
                        <Select options={providers} onChange={handleSelectChange} />
                    </Col>
                </Row>
                <button onClick={addRow}>Добавить элемент заказа</button>
                {
                    rows.length > 0 &&
                    rows.map((obj, index) => {
                        return (
                            <div key={index}>
                                <input
                                    name='name'
                                    placeholder='Название товара'
                                    value={obj.name}
                                    onChange={event => handleFormChange(index, event)}
                                />
                                <input
                                    name='quantity'
                                    placeholder='Количество'
                                    value={obj.quantity}
                                    onChange={event => handleFormChange(index, event)}
                                />
                                <input
                                    name='unit'
                                    placeholder='Единица измерения'
                                    value={obj.unit}
                                    onChange={event => handleFormChange(index, event)}
                                />
                                <button onClick={() => removeFields(index)}>Remove</button>
                            </div>
                        )
                    })
                }
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={() => setOpen(!open)}>
                    Close
                </Button>
                <Button variant="primary" onClick={handleSubmit}>
                    Save Changes
                </Button>
            </Modal.Footer>
        </Modal >
    )
};

export default OrderForm;