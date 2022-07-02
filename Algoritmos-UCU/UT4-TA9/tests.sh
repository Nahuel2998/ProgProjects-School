#!/bin/bash

altas='altasPrueba.txt'
elim='elimPrueba.txt'
ventas='ventasPrueba.txt'

# Agregar desde Archivo
awk -F, '{sum += $3 * $4} END{print "Monto total:", sum}' ${altas}

# Eliminar desde Archivo
awk -F, '{sum += $3 * $4} END{print "Perdidas:", sum}' <(grep -f ${elim} ${altas})

# Vender desde Archivo
awk -F, '{sum += $3 >= $4 ? $2 * $4 : 0} END{print "Ganancias:", sum}' <(join -t, <(awk -F, '{stock[$1] += $4; precio[$1] = $3} END{for (id in stock) {print id "," precio[id] "," stock[id]}}' <(grep -f <(cut -d, -f1 ${ventas}) ${altas}) | sort) <(sort ${ventas}))
