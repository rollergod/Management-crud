import React from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

import Select from 'react-select';
import axios from 'axios';
import { SingleValue } from 'react-select/dist/declarations/src';

interface Option {
    value: string
}

const OrderForm = ({ open, setOpen, headerText, orderId = 0 }) => {

    const handleClose = () => setOpen(false);
    const [number, setNumber] = React.useState('');
    const [providerId, setProviderId] = React.useState(0);
    const [items, setItems] = React.useState([{
        name: '', quantity: '', unit: ''
    }]);
    const [providers, setProviders] = React.useState([{}]);


    const [editOrderItemNumber, setEditOrderItemNumber] = React.useState('');
    const [editOrderItemProviderId, setEditOrderItemProviderId] = React.useState(0);
    const providersArrForLabel = providers.map(function (i) { return { value: i.id, label: i.name } });

    React.useEffect(() => {

        axios.get(`https://localhost:7212/api/provider`)
            .then((response) => {
                setProviders(response.data);
            })
            .catch((error) => {
                console.log('error', error);
            })

        if (orderId > 0) {
            axios.get(`https://localhost:7212/api/order/${orderId}/orderItems`)
                .then((response) => {
                    setEditOrderItemNumber(response.data.number)
                    setEditOrderItemProviderId(response.data.providerId)
                    setItems(response.data.items)
                    console.log('orderItem', response.data);
                })
                .catch((error) => {
                    console.log('error', error);
                })
        }
    }, []);

    const handleSubmit = () => {
        if (orderId > 0) {
            console.log(items);
            const data = {
                'id': orderId,
                'number': editOrderItemNumber,
                'providerId': editOrderItemProviderId,
                'items': items
            };
            axios.put(`https://localhost:7212/api/order/${orderId}`, data)
                .then((resp) => {
                    if (resp.status === 200)
                        setOpen(false);
                });
        }
        else {
            const data = {
                'number': number,
                'providerId': providerId,
                'items': items
            };
            axios.post("https://localhost:7212/api/order", data)
                .then((resp) => {
                    if (resp.status === 200)
                        setOpen(false);
                });
        }
    }

    const handleRemove = (id: number) => {
        axios.delete(`https://localhost:7212/api/order/${orderId}`)
            .then((response) => {
                console.log('delete', response);
                setOpen(false);
            })
            .catch((error) => {
                console.log('error', error);
            })
    }

    const handleFormChange = (index: number, event: React.ChangeEvent<HTMLInputElement>) => {
        let data = [...items];
        data[index][event.target.name] = event.target.value;
        setItems(data);
    };

    const addRow = () => {
        let newItem = { name: '', quantity: '', unit: '' }

        setItems([...items, newItem]);
    }

    const removeFields = (index: number) => {
        let data = [...items];
        data.splice(index, 1)
        setItems(data)
    }

    const handleSelectChange = (option: SingleValue<Option>) => {
        setProviderId(Number(option?.value));
    }

    return (
        <Modal show={open} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>{headerText}</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Row>
                    <Col>
                        {
                            orderId > 0 ?
                                <input type="text" className='form-control' placeholder='Введите номер заказа' value={editOrderItemNumber} onChange={(e) => setEditOrderItemNumber(e.target.value)} />
                                : <input type="text" className='form-control' placeholder='Введите номер заказа' value={number} onChange={(e) => setNumber(e.target.value)} />
                        }
                    </Col>
                    <Col>
                        <Select options={providersArrForLabel} value={providersArrForLabel.find(el => el.value === editOrderItemProviderId)} onChange={handleSelectChange} />
                    </Col>
                </Row>
                <button onClick={addRow}>Добавить элемент заказа</button>
                {
                    items.length > 0 &&
                    items.map((obj, index) => {
                        return (
                            <div key={index}>
                                <input
                                    name='name'
                                    placeholder='Название товара'
                                    value={obj.name}
                                    onChange={event => handleFormChange(index, event)}
                                />
                                <input
                                    type='number'
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
                    Закрыть форму
                </Button>
                <Button variant="primary" onClick={handleSubmit}>
                    Сохранить
                </Button>
                {
                    orderId > 0 &&
                    <Button variant="danger" onClick={() => handleRemove(orderId)}>
                        Удалить заказ
                    </Button>
                }
            </Modal.Footer>
        </Modal >
    )
};

export default OrderForm;