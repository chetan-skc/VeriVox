import React from 'react'
import ShortText from './ShortText'
import NumberInput from './NumberInput'
import BigText from './BigText'
import Ratings from './Ratings'
import Dropdown from './Dropdown'
import RadioButtons from './RadioButtons'
import TypeAhead from './TypeAhead'
import CheckBox from './CheckBox'


const SelectedOption = ({answerType, option, setOption }) => {

    

  switch (answerType){
    case "ShortText":
        return <ShortText/>;
    case 'NumberInput':
        return <NumberInput/>;
    case 'BigText':
        return <BigText/>;
    case 'Ratings':
        return <Ratings/>;
    case 'Dropdown':
        return <Dropdown
        option={option} setOption={setOption}/>;
    case 'RadioButtons':
        return <RadioButtons
        option={option} setOption={setOption}/>;
    case 'TypeAhead':
        return <TypeAhead
        option={option} setOption={setOption}/>;
    case 'CheckBox':
        return <CheckBox 
        option={option} setOption={setOption}/>;
    default:
        return null;
    }
}

export default SelectedOption;
