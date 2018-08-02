contract ExampleContract {
    
    event execution(address _from, uint256 _value);
    
    uint256 public myValue = 0;
    
    function Execute(uint256 newValue) public {
        myValue = newValue;
        execution(msg.sender, newValue);
    } 
    
}