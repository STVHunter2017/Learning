class FM27():
    def __init__ (self):
        self.NormalDutyVapourPressureFactor = 1.3
        
    def RunFM27(self, state):
       
        PCmax = state['PCmax']
        PCavg = state['PCavg']
        PCMin = state['PCMin']
        Pvap = state['Pvap']
        SealType = state['SealType']
        dPmin = CalculatedPmin();

        return dPmin

    def CalculatedPmin():
        normalDutySeal_dPmin = (NormalDutyVapourPressureFactor * Pvap)
