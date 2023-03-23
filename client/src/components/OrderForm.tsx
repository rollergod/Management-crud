import React from 'react';
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';

import Select from 'react-select';
import axios from 'axios';

const OrderForm = ({ open, setOpen, headerText, orderId = 0 }) => {

    const handleClose = () => setOpen(false);
    const [number, setNumber] = React.useState<string>('');
    const [items, setItems] = React.useState([{
        name: '', quantity: '', unit: ''
    }]);
    const [providers, setProviders] = React.useState([{
        id: '', name: ''
    }]);

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
                })
                .catch((error) => {
                    console.log('error', error);
                })
        }
    }, []);

    const handleSelectChange = (option: any) => {
        setEditOrderItemProviderId(Number(option?.value));
    }

    const getValue = () => {
        return editOrderItemProviderId ? providersArrForLabel.find(c => Number(c.value) === editOrderItemProviderId) : '';
    };

    const handleSubmit = () => {
        if (orderId > 0) {
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
                'providerId': editOrderItemProviderId,
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

    return (
        <Modal show={open} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>{headerText}</Modal.Title>
            </Modal.Header>
            <Modal.Body className='text-center'>
                <Row>
                    <Col>
                        {
                            orderId > 0 ?
                                <div>
                                    <label>Name</label>
                                    <input
                                        type="text"
                                        className='form-control text-center'
                                        placeholder='Введите номер заказа'
                                        value={editOrderItemNumber}
                                        onChange={(e) => setEditOrderItemNumber(e.target.value)}
                                    />
                                </div>
                                :
                                <div>
                                    <label>Name</label>
                                    <input
                                        type="text"
                                        className='form-control text-center'
                                        placeholder='Введите номер заказа'
                                        value={number}
                                        onChange={(e) => setNumber(e.target.value)}
                                    />
                                </div>
                        }
                    </Col>
                    <Col>
                        <label>Provider</label>
                        <Select
                            options={providersArrForLabel}
                            value={getValue()}
                            onChange={handleSelectChange}
                        />
                    </Col>
                </Row>
                <button className='btn btn-primary mt-3' onClick={addRow}>Добавить элемент заказа</button>
                {
                    items.length > 0 &&
                    items.map((obj, index) => {
                        return (
                            <div key={index} className='mt-3'>
                                <div className='mb-1'>
                                    <label>OrderItem Name</label>
                                    <input
                                        className='form-control w-50'
                                        name='name'
                                        placeholder='Название товара'
                                        value={obj.name}
                                        onChange={event => handleFormChange(index, event)}
                                    />
                                </div>
                                <div className='mb-1'>
                                    <label>OrderItem Quantity</label>
                                    <input
                                        className='form-control w-50'
                                        type='number'
                                        name='quantity'
                                        placeholder='Количество'
                                        value={obj.quantity}
                                        onChange={event => handleFormChange(index, event)}
                                    />
                                </div>
                                <div className='mb-1'>
                                    <label>OrderItem Unit</label>
                                    <input
                                        className='form-control w-50'
                                        name='unit'
                                        placeholder='Единица измерения'
                                        value={obj.unit}
                                        onChange={event => handleFormChange(index, event)}
                                    />
                                </div>
                                <button className='btn btn-danger mt-2' onClick={() => removeFields(index)}>Remove</button>
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