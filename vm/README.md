<h1>Simple Virtual Machine</h1>

<p>
A novelty, toy virtual machine/interpreter that executes a custom bytecode language, complete with simplistic
assembler to convert a simple assembly language into executable bytecode.
</p>

<p>Author: Damon Swayn</p>
<p>License: BSD</p>

<h3>Instructions</h3>

<h4>runtime modes</h4>

<p>
vm.exe can run in one of three modes: compile-mode, run-mode and interpret-mode.
</p>

<p>compile-mode (inputfile is a file containing simple assembly, and outputfile is the file you wish to write compiled bytecode too.</p>
<code>vm.exe -c inputfile outputfile</code>

<p>run-mode (input file is a file containing previously compiled bytecode)</p>
<code>vm.exe -r inputfile</code>

<p>interpret-mode (input file is a file containing simple assembly)</p>
<code>vm.exe -i inputfile</code>

<h4>simple assembly</h4>

<p>simple assembly has 14 keywords, each taking two parameters</p>

<p>There are 5 arithmatic operations, each take the parameters addr1 and addr2 which are the addresses in memory of the values you wish to operate on. The results are then stored into register A.<p>

<ul>
	<li>add addr1 addr2</li>
	<li>sub addr1 addr2</li>
	<li>mul addr1 addr2</li>
	<li>div addr1 addr2</li>
	<li>mod addr1 addr2</li>
</ul>

<p>There are also 5 comparison operators, these take parameters addr1 and addr2 which are the addresses in memory of the values you wish to operate on. The results are stored in register BF.</p>

<ul>
	<li>clt addr1 addr2 (<)</li>
	<li>cgt addr1 addr2 (>)</li>
	<li>ceq addr1 addr2 (==)</li>
	<li>cle addr1 addr2 (<=)</li>
	<li>cge addr1 addr2 (>=)</li>
</ul>

<p>The str operator (str val addr), takes a value as param1 and stores it in the memory address provided as param2.</p>

<p>The streg operator (streg val reg), takes a value as param1 and stores it in the register indicated in param 2 (see notes for explanation of reg flags)</p>

<p>The ldreg operator (ldreg addr reg), takes the value from the register indicated in param2 and stores it in the memory address indicated in param1</p>

<p>The strnxt operator (strnxt flag val), stores the indicated value in the next available free block in memory. flag indicates whether val refers to a address, register or raw value. (see notes for explanation of flag)</p>

<p>the jmp operator (jmp lineT lineF), jumps to lineT if the bool flag is set to true or it will jump to lineF if the bool flag is set to false. The params represent line numbers where one instruction (plus params) equates to one line.</p>

<h3>Notes</h3>

<ul>
	<li>a register flag (reg parameter) means the parameter can either be 1, 2, 3 or 4 indicating register A, B, C or D respectively.</li>
	<li>a flag value (flag parameter) means the parameter can be either 1, 2 or 3 indicating that the provided value is either a memory address, a register flag (see above) or a raw value.</li>
	<li>performing a jmp operator will reset the bool flag (BF) to the value of the operating systems byte.MaxValue attribute, in most cases, this will be 255.</li>
	<li>performing a jmp operator without setting the BF will result in an exception.</li>
	<li>next version will add the option to access the BF as a register flag option.</li>
</ul>
