import abc
import math
import sys
from typing import Sequence, Optional, Any

ActionId = str
UCB1_C = math.sqrt(2)

class AbstractMCState(abc.ABC):

    problem: dict[Any, Any]

    parent: Optional['AbstractMCState']
    children: dict[ActionId, 'AbstractMCState']
    value_estimate: float

    nr_simulations = 0

    def __init__(
            self,
            problem: dict,
            parent: Optional['AbstractMCState'] = None,
            children: dict[ActionId, 'AbstractMCState'] = None,
            value_estimate: float = -1
    ):
        self.problem = problem
        self.parent = parent
        self.children = children if children is not None else dict()
        self.value_estimate = value_estimate

    @abc.abstractmethod
    def is_winning(self) -> bool:
        pass

    @abc.abstractmethod
    def is_terminal(self) -> bool:
        pass

    @abc.abstractmethod
    def calc_terminal_value(self) -> float:
        pass

    @abc.abstractmethod
    def get_available_actions(self) -> Sequence[ActionId]:
        pass

    @abc.abstractmethod
    def apply_action(self, action_id: ActionId) -> 'AbstractMCState':
        pass

    def expand(self):
        for action_id in self.get_available_actions():
            self.children[action_id] = self.apply_action(action_id)

    def calc_ucb1(self):
        if self.nr_simulations == 0:
            return sys.maxsize
        return self.value_estimate + UCB1_C * (math.log(self.parent.nr_simulations)/self.nr_simulations)**0.5

    def is_leaf(self):
        return self.nr_simulations == 0

    def propagate_simulation_result(self, value: float):
        self.value_estimate = max(self.value_estimate, value)
        self.nr_simulations += 1
        if self.parent is not None:
            self.parent.propagate_simulation_result(value)
