import React, { useState, useEffect } from 'react';
import axios from "axios";

const Home = () => {
    const [people, setPeople] = useState([]);
    useEffect(() => {
        const getPeople = async () => {
            const { data } = await axios.get('api/people/getpeople');
            setPeople(data);
        }
        getPeople();
    }, []);

    const onDeleteClick = async () => {
        await axios.post('api/people/delete');
        setPeople([]);

    }
    return (
        <>
            <div className='row'>
                <div className='col-md-6'>
                    <button className='btn btn-block btn-danger btn-lg' onClick={onDeleteClick}>Delete All</button>
                </div>
            </div>
            <table className='table table-hover table-striped table-bordered'>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>First Name</th>
                        <th>Last Name</th>
                        <th>Age</th>
                        <th>Address</th>
                        <th>Email</th>
                    </tr>
                </thead>
                <tbody>
                    {people.map(p => <tr>
                        <td>{p.id}</td>
                        <td>{p.firstName}</td>
                        <td>{p.lastName}</td>
                        <td>{p.age}</td>
                        <td>{p.address}</td>
                        <td>{p.email}</td>
                    </tr>)}
                </tbody>
            </table>

        </>
    )


}
export default Home;