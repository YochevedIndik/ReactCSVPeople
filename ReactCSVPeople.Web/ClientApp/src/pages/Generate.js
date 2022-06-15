import React, { useState } from "react";

const Generate = () => {
    const [amount, setAmount] = useState(0);
    const onGenerateClick = async () => {
        window.location = `/api/people/generatepeople?amount=${amount}`;
    }
    return (

        <div className="container">
            <div className="col-md-5">
                <input type="text" className="form-control-lg" placeholder="Amount" onChange={e => setAmount(e.target.value)}></input>

                <button className="btn-primary btn-lg" onClick={onGenerateClick}>Generate</button>
            </div>
        </div>


    )
}
export default Generate;