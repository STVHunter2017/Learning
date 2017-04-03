import json

#results1 = json.dumps('{ state : [ {"X" : "10"}, {"Y":"20"} ] }')
results1 = json.dumps('{"X" : "10", "Y" : "20"}')        
#results =json.loads(results1)
results =json.loads('{"X" : "10", "Y" : "20"}')        

print (results)
print (results1)

print (results['X'])
print (results['Y'])

print (results)
#Team City

